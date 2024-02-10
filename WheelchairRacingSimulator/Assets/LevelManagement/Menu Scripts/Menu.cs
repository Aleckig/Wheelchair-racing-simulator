using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelchairGame;

namespace LevelManagement
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.LoadNextLevel();
            }
            else
            {
                Debug.LogError("Game Manager not found in the scene.");
            }

        }
        public void OnSettingsPressed()
        {
            MenuManager menuManager = FindObjectOfType<MenuManager>();
            Menu settingsMenu = transform.parent.Find("SettingsMenu(Clone)").GetComponent<Menu>();

            if (menuManager != null && settingsMenu != null)
            {
                menuManager.OpenMenu(settingsMenu);
            }
            else
            {
                Debug.LogError("Menu Manager or Settings Menu not found in the scene.");
            }
            //Debug.Log("Settings");
        }
        public void OnCreditsPressed()
        {
            MenuManager menuManager = FindObjectOfType<MenuManager>();
            Menu creditsMenu = transform.parent.Find("CreditsScreen(Clone)").GetComponent<Menu>();

            if (menuManager != null && creditsMenu != null)
            {
                menuManager.OpenMenu(creditsMenu);
            }
            else
            {
                Debug.LogError("Menu Manager or Credits Menu not found in the scene.");
            }
            //Debug.Log("Credits");
        }
        public void OnBackPressed()
        {
            MenuManager menuManager = FindObjectOfType<MenuManager>();
            if (menuManager != null)
            {
                menuManager.CloseMenu();

                Debug.Log("Back");
            }
            else
            {
                Debug.LogError("Menu Manager not found in the scene.");
            }
        }   
    }

}


