using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;
using UnityEngine.Audio;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [Header("Volume")]

        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private AudioMixer volumeMixer;

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

        public void OnMasterVolumeChanged(float volume)
        {
            if (dataManager != null)
            {
                dataManager.MasterVolume = volume;
            }

            if (volume > 0)
            {
                volumeMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            }
            else
            {
                // Set volume to silence if slider is at 0
                volumeMixer.SetFloat("Master", -80); // Adjust -80 according to your desired silence level
            }
        }

        public void OnMusicVolumeChanged(float volume)
        {
            if (dataManager != null)
            {
                dataManager.MusicVolume = volume;
            }

            if (volume > 0)
            {
                volumeMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
            }
            else
            {
                // Set volume to silence if slider is at 0
                volumeMixer.SetFloat("Music", -80); // Adjust -80 according to your desired silence level
            }
        }

        public void OnSFXVolumeChanged(float volume)
        {
            if (dataManager != null)
            {
                dataManager.SFXVolume = volume;
            }

            if (volume > 0)
            {
                volumeMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
            }
            else
            {
                // Set volume to silence if slider is at 0
                volumeMixer.SetFloat("SFX", -80); // Adjust -80 according to your desired silence level
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            if (dataManager != null)
            {
                dataManager.Save();
            }
        }

        public void LoadData()
        {
            if (dataManager == null || masterVolumeSlider == null || musicVolumeSlider == null || sfxVolumeSlider == null)
            {
                return;
            }
            dataManager.Load();
            
                
            
            masterVolumeSlider.value = dataManager.MasterVolume;
            musicVolumeSlider.value = dataManager.MusicVolume;
            sfxVolumeSlider.value = dataManager.SFXVolume;

        }
    }
}
