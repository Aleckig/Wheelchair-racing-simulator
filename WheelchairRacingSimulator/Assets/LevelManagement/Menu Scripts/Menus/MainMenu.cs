using System.Collections;
using System.Collections.Generic;
using LevelManagement;
using UnityEngine;
using WheelchairGame;
using UnityEngine.UI;
using LevelManagement.Data;
using TMPro; 


namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField] private float playDelay = 0.5f;
        [SerializeField] private TransitionFader startTransitionPrefab;
        [SerializeField] private TMP_InputField inputField;
        private DataManager dataManager;

        protected override void Awake() 
        {
            base.Awake();
            dataManager = Object.FindObjectOfType<DataManager>();
        }
        private void Start()
        {
            LoadData();
        }
        private void LoadData()
        {
            if (dataManager != null && inputField != null)
            {
                dataManager.Load();
                inputField.text = dataManager.PlayerName;
            }
        }
        public void OnPlayerNameValueChanged(string name)
        {
            if (dataManager != null)
            {
                dataManager.PlayerName = name;
            }
        }
        public void OnPlayerNameEndEdit(string name)
        {
            if(dataManager != null)
            {
                dataManager.Save();
            }
        }

         public void OnPlayPressed()
        {
           StartCoroutine(OnPlayPressedRoutine());
        }
        private IEnumerator OnPlayPressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadNextLevel();
            yield return new WaitForSeconds(playDelay);
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

