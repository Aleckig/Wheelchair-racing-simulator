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

            GameMenu.Open();
        }
        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }
        public void OnCreditsPressed()
        {
            CreditScreen.Open();
        }
        public void OnQuitPressed()
        {
            
            Application.Quit();
            //Debug.Log("Quit");
        }
    }
}

