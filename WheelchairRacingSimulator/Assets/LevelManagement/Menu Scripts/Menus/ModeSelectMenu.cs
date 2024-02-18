using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Modes;
using TMPro;



namespace LevelManagement
{
    [RequireComponent(typeof(ModeSelector))]
    public class ModeSelectMenu : Menu<ModeSelectMenu>
    {
        [SerializeField] protected TMP_Text nameText;
        [SerializeField] protected TMP_Text descriptionText;
        [SerializeField] protected Image image;
        [SerializeField] protected TMP_Text bestTimeText;

        [SerializeField] protected float playDelay = 0.5f;
        [SerializeField] protected TransitionFader startTransitionPrefab;

        protected ModeSelector modeSelector;
        protected ModeSpecs currentMode;

        protected override void Awake()
        {
            base.Awake();
            modeSelector = GetComponent<ModeSelector>();
            UpdateInfo();
        }

        private void OnEnable()
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            currentMode = modeSelector.GetCurrentMode();
    
            nameText.text = currentMode?.Name;
            descriptionText.text = currentMode?.Description;
            image.sprite = currentMode?.Image;
            bestTimeText.text = currentMode?.BestTime;
           
        }

        public void OnNextPressed()
        {
            modeSelector.IncrementIndex();
            UpdateInfo();
        }

        public void OnPreviousPressed()
        {
            modeSelector.DecrementIndex();
            UpdateInfo();
        }

        public void OnPlayPressed()
        {
            StartCoroutine(OnPlayPressedRoutine(currentMode?.SceneName));
        }

        private IEnumerator OnPlayPressedRoutine(string sceneName)
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            LevelLoader.LoadLevel(sceneName);
            yield return new WaitForSeconds(playDelay);
            GameMenu.Open();
        }
       
    
    }
}