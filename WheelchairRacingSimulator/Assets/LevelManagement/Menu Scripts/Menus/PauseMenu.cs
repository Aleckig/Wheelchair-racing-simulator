using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelchairGame;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
         public void OnResumePressed()
        {
            Time.timeScale = 1;
            base.OnBackPressed();
        }

        public void OnRestartPressed()
        {
            
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            base.OnBackPressed();
        }

        public void OnMainMenuPressed()
        {
            
            Time.timeScale = 1;
            LevelLoader.LoadMainMenuLevel();

            MainMenu.Open();
           
              
        }
        public void OnSettingsPressend()
        {
            SettingsMenu.Open();
        }

        public void OnQuitPressed()
        {
            Application.Quit();
        }

        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnResumePressed();
            }
        }
    }

}
