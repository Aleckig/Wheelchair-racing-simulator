using System.Collections;
using System.Collections.Generic;
using LevelManagement;
using UnityEngine;
using WheelchairGame;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
         public void OnPlayPressed()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadNextLevel();
            }

            if (MenuManager.Instance != null && GameMenu.Instance != null)
            {
                MenuManager.Instance.OpenMenu(GameMenu.Instance);
            }
        }
        public void OnSettingsPressed()
        {
            if (MenuManager.Instance != null && SettingsMenu.Instance!= null)
            {
                MenuManager.Instance.OpenMenu(SettingsMenu.Instance);
            }
            else
            {
                Debug.LogError("Menu Manager or Settings Menu not found in the scene.");
            }
            //Debug.Log("Settings");
        }
        public void OnCreditsPressed()
        {
            if (MenuManager.Instance != null && CreditScreen.Instance != null)
            {
                MenuManager.Instance.OpenMenu(CreditScreen.Instance);
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

