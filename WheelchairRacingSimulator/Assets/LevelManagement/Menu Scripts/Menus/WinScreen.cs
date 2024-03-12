using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using TMPro;
using WheelchairGame;


namespace LevelManagement
{
    public class WinScreen : Menu<WinScreen>
    {
        [SerializeField] private TextMeshProUGUI timerText;

        // Variable to store the timer value
        private float timerValue;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Reset the timer when a new scene is loaded
            ResetTimer();
        }



        // Method to update the timer text
        public void UpdateTimerText(float timerValue)
        {
            this.timerValue = timerValue;
            timerText.text = FormatTimerText(timerValue);
        }

        // Reset the timer to its initial state
        public void ResetTimer()
        {
            // Set the timer value to its initial value (0 seconds)
            timerValue = 0f;

            // Update the timer text with the initial value
            UpdateTimerText(timerValue);
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
            // Change this later
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
            // Retrieve the current race time from GameManager
            float currentRaceTime = GameManager.Instance.Timer;

            // Update the timer text with the current race time
            UpdateTimerText(currentRaceTime);

            // Reset the timer before going back to the main menu
            ResetTimer();

            // Call CloseMenu from MenuManager to properly close and handle the destruction
            MenuManager.Instance.CloseMenu();

            // Load the main menu scene (assuming scene 0 is the main menu)
            LevelLoader.LoadLevel(1);

            // Open the MainMenu
            MainMenu.Open();
        }
        /*
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
        */


    }
}

