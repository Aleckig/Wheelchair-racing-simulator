using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement
{
    public class SettingsMenu : Menu<SettingsMenu>
    {

        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        protected override void Awake()
        {
            base.Awake();
            
        }

        public void OnMasterVolumeChanged(float volume)
        {
            
        }
        public void OnMusicVolumeChanged(float volume)
        {
            
        }   
        public void OnSFXVolumeChanged(float volume)
        {
            
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}
