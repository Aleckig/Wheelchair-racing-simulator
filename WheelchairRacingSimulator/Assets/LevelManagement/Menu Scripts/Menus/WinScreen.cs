using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using TMPro;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        [SerializeField] private TextMeshProUGUI timerText;

        // Method to update the timer text
        public void UpdateTimerText(float timerValue)
        {
            timerText.text = FormatTimerText(timerValue);
        }

        private string FormatTimerText(float timerValue)
        {
            int minutes = Mathf.FloorToInt(timerValue / 60);
            int seconds = Mathf.FloorToInt(timerValue % 60);
            int milliseconds = Mathf.FloorToInt((timerValue * 1000) % 1000);

            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
        public void OnLoadLevelSelect()
        {
            //change this later
            base.OnBackPressed();
            ModeSelectMenu.Open();
        }

        public void OnRestartPressed()
        {
            base.OnBackPressed();
            LevelLoader.ReloadLevel();
        }   

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }
    }
}

