using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelchairGame;

namespace LevelManagement
{
    // Base class for generic menus
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        // Awake is called when the script instance is being loaded
        protected virtual void Awake()
        {
            // If an instance of this menu already exists, destroy the new instance
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                // Set this instance as the singleton instance
                instance = (T)this;
            }
        }
        public static void Open()
        {
            if (MenuManager.Instance != null && instance != null)
            {
                MenuManager.Instance.OpenMenu(instance);
            }
            else
            {
                Debug.LogError("Menu Manager not found in the scene.");
            }
            
        }

        // OnDestroy is called when the MonoBehaviour will be destroyed
        protected virtual void OnDestroy()
        {
            // If this instance is the current singleton instance, set it to null
            if (instance == this)
            {
                instance = null;
            }
        }
    }

    // Base class for menus
    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        // Called when the back button is pressed
        public virtual void OnBackPressed()
        {
            // If MenuManager exists, close the current menu
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
                Debug.Log("Back");
            }
            else
            {
                // Log an error if MenuManager is not found
                Debug.LogError("Menu Manager not found in the scene.");
            }
        }
    }


}


