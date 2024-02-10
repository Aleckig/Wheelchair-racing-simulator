using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelchairGame;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class PauseMenu : Menu<PauseMenu>
    {
        [SerializeField] private int mainMenuIndex = 0 ;
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
            SceneManager.LoadScene(mainMenuIndex);

            if(MenuManager.Instance != null && MainMenu.Instance != null)
            {
                MenuManager.Instance.OpenMenu(MainMenu.Instance);
            }
            else
            {
                Debug.LogError("Menu Manager or Main Menu not found in the scene.");
            }
           
              
        }
        public void OnSettingsPressend()
        {
            //SettingsMenu.Open();
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
