using System.Collections;
using System.Collections.Generic;
using LevelManagement;
using UnityEngine;
using WheelchairGame;

namespace LevelManagement
{
    public class MainMenu : Menu
    {
        public void OnPlayPressed()
        {
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }
            else
            {
                Debug.LogError("Game Manager not found in the scene.");
            }

        }
        public void OnSettingsPressed()
        {
            
            Menu settingsMenu = transform.parent.Find("SettingsMenu(Clone)").GetComponent<Menu>();

            if (MenuManager.Instance != null && settingsMenu != null)
            {
                MenuManager.Instance.OpenMenu(settingsMenu);
            }
            else
            {
                Debug.LogError("Menu Manager or Settings Menu not found in the scene.");
            }
            //Debug.Log("Settings");
        }
        public void OnCreditsPressed()
        {
            
            Menu creditsMenu = transform.parent.Find("CreditsScreen(Clone)").GetComponent<Menu>();

            if (MenuManager.Instance != null && creditsMenu != null)
            {
                MenuManager.Instance.OpenMenu(creditsMenu);
            }
            else
            {
                Debug.LogError("Menu Manager or Credits Menu not found in the scene.");
            }
            //Debug.Log("Credits");
        }
        public void OnQuitPressed()
        {
            
            Application.Quit();
            //Debug.Log("Quit");
        }
    }
}

