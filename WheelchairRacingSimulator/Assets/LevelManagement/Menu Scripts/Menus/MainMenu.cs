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
           ModeSelectMenu.Open();
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

