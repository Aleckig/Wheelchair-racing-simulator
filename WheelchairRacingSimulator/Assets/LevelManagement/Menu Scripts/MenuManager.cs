using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace LevelManagement
{
    
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenu mainMenuPrefab;
        [SerializeField] private SettingsMenu settingsMenuPrefab;
        [SerializeField] private CreditScreen creditsMenuPrefab;
        [SerializeField] private PauseMenu pauseMenuPrefab;
        [SerializeField] private GameMenu gameMenuPrefab;
        [SerializeField] private WinScreen winScreenPrefab;   

        [SerializeField] private Transform menuParent;

        private Stack<Menu> menuStack = new Stack<Menu>();
        private static MenuManager instance;
        public static MenuManager Instance { get { return instance; } }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                InitializeMenus();
                Object.DontDestroyOnLoad(gameObject);
            }
            
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        private void InitializeMenus()
        {
            // Check if menuParent is not assigned in the Inspector
            if (menuParent == null)
            {
                // Create a new GameObject to serve as the parent for menus
                GameObject menuParentObject = new GameObject("Menus");
                menuParent = menuParentObject.transform;
            }
            DontDestroyOnLoad(menuParent.gameObject);

            // Array of menu prefabs
            //Menu[] menuPrefabs = { mainMenuPrefab, settingsMenuPrefab, creditsMenuPrefab, pauseMenuPrefab, gameMenuPrefab, winScreenPrefab}; 
            System.Type myType = this.GetType();
            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = myType.GetFields(myFlags);
            
            // Instantiate and initialize each menu
            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;
                if (prefab != null)
                {
                    // Instantiate the menu prefab
                    Menu menuInstance = Instantiate(prefab, menuParent);

                    // Check if the menu is not the main menu, then deactivate it
                    if (prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        OpenMenu(menuInstance);
                    }
                }
            }
            
        }

        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning("MENUMANAGER OpenMenu <<invalid menu>>");
                return;
            }

            // Deactivate the top menu on the stack if it exists
            if (menuStack.Count > 0)
            {
                foreach (Menu menu in menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }

            // Activate the new menu
            menuInstance.gameObject.SetActive(true);

            // Push the new menu onto the stack
            menuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if (menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu <<no menus in stack>>");
                return;
            }

            // Deactivate the top menu on the stack
            Menu topMenu = menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            // Activate the new top menu on the stack
            if (menuStack.Count > 0)
            {
                Menu nextMenu = menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    }
}