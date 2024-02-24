using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using LevelManagement;
using TMPro;

namespace WheelchairGame
{
    public class GameManager : MonoBehaviour
    {
            // Reference to the player GameObject
        [SerializeField] private GameObject player;

        // Reference to the Objective script
        private Objective objective;

        // Flag to track if the game is over
        private bool isGameOver;

        // Singleton instance of the GameManager
        private static GameManager instance;

        // Reference to the transition fader prefab
        [SerializeField] private TransitionFader endTransitionPrefab;

        // Countdown variables
        [SerializeField] private float countdownDuration = 5f;
        [SerializeField] private TextMeshProUGUI countdownText;
        private bool isCountdownFinished = false; // Flag to track countdown status

        // Timer variables
        [SerializeField] private TextMeshProUGUI timerText;
        private float timer;
        [SerializeField] private TestMovement testMovement;

        // Singleton instance property
        public static GameManager Instance { get { return instance; } }

        private void Awake()
        {
            // Singleton pattern implementation
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            // Find the Objective script in the scene
            objective = FindObjectOfType<Objective>();
            if (objective == null)
            {
                Debug.LogError("Objective not found in the scene.");
            }
        }

        private void OnDestroy()
        {
            // Clear the singleton instance on destroy
            if (instance == this)
            {
                instance = null;
            }
        }

        private void Start()
        {
            // Start the countdown routine
            testMovement.enabled = false;
            StartCoroutine(CountdownRoutine());
        }

        private IEnumerator CountdownRoutine()
        {
            float countdownTime = countdownDuration;

            // Countdown loop
            while (countdownTime > 0)
            {
                countdownText.text = Mathf.CeilToInt(countdownTime).ToString();
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }

            // Finish countdown
            countdownText.text = "GO!";
            yield return new WaitForSeconds(1f);
            countdownText.gameObject.SetActive(false);

            // Set the flag to indicate countdown finished
            isCountdownFinished = true;
            testMovement.enabled = true;

            // Start the timer
            StartCoroutine(TimerRoutine());
        }

        private IEnumerator TimerRoutine()
        {
            // Timer loop
            while (!isGameOver)
            {
                if (isCountdownFinished) // Only update the timer when the countdown has finished
                {
                    timer += Time.deltaTime;
                    UpdateTimerText();
                }
                yield return null;
            }
        }

        private void UpdateTimerText()
        {
            // Update the timer text
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

        public void EndLevel()
        {
            // End the level
            if (!isGameOver)
            {
                isGameOver = true;
                StopAllCoroutines(); // Stop the countdown and timer routines
                StartCoroutine(WinRoutine());
                Debug.Log("Level Complete");
            }
        }

        private IEnumerator WinRoutine()
        {
            // Play transition and open win screen
            TransitionFader.PlayTransition(endTransitionPrefab);

            float fadeDelay = (endTransitionPrefab != null) ? endTransitionPrefab.Delay + endTransitionPrefab.FadeOnDuration : 0f;
            yield return new WaitForSeconds(fadeDelay);
            WinScreen.Open();
        }

        private void Update()
        {
            // Check if the objective is complete and the countdown has finished
            if (objective != null && objective.IsComplete && isCountdownFinished)
            {
                EndLevel();
            }
        }
    }

}

