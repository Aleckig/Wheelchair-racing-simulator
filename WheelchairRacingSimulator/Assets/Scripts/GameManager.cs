using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;
using TMPro;
using WheelchairGame;

namespace WheelchairGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private Objective objective;
        private bool isGameOver;
        private static GameManager instance;

        [SerializeField] private TransitionFader endTransitionPrefab;
        [SerializeField] private float countdownDuration = 5f;
        [SerializeField] private TextMeshProUGUI countdownText;
        private bool isCountdownFinished = false;
        
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI savedTimeText; // UI element for saved time
        [SerializeField] private TextMeshProUGUI loadedTimeText;
        
        public float timer;
        public float Timer => timer;
        [SerializeField] private TestMovement testMovement;
        public float CurrentTimer => timer;

        private const string FinalTimeKey = "FinalTime";

        public static GameManager Instance { get { return instance; } }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            objective = FindObjectOfType<Objective>();
            if (objective == null)
            {
                Debug.LogError("Objective not found in the scene.");
            }
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        private void Start()
        {
            testMovement.enabled = false;
            StartCoroutine(CountdownRoutine());

            float savedTime = LoadFinalTime();
            UpdateTimerText();
        }

        private IEnumerator CountdownRoutine()
        {
            float countdownTime = countdownDuration;

            while (countdownTime > 0)
            {
                countdownText.text = Mathf.CeilToInt(countdownTime).ToString();
                yield return new WaitForSeconds(1f);
                countdownTime--;
            }

            countdownText.text = "GO!";
            yield return new WaitForSeconds(1f);
            countdownText.gameObject.SetActive(false);

            isCountdownFinished = true;
            testMovement.enabled = true;

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
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

            timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

            // Load the saved final time
            float loadedTime = LoadFinalTime();

            // Display loaded time
            int loadedMinutes = Mathf.FloorToInt(loadedTime / 60);
            int loadedSeconds = Mathf.FloorToInt(loadedTime % 60);
            int loadedMilliseconds = Mathf.FloorToInt((loadedTime * 1000) % 1000);

            loadedTimeText.text = string.Format("Best Time: {0:00}:{1:00}:{2:000}", loadedMinutes, loadedSeconds, loadedMilliseconds);

            // Compare the current timer with the loaded time
            if (timer < loadedTime || loadedTime == 0f)
            {
                // If the new time is faster or there's no loaded time, update the saved time text
                int savedMinutes = Mathf.FloorToInt(timer / 60);
                int savedSeconds = Mathf.FloorToInt(timer % 60);
                int savedMilliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

                savedTimeText.text = string.Format("Saved Time: {0:00}:{1:00}:{2:000}", savedMinutes, savedSeconds, savedMilliseconds);
            }
        }

        public void EndLevel()
        {
            if (!isGameOver)
            {
                isGameOver = true;
                StopAllCoroutines();
                StartCoroutine(WinRoutine());

                SaveFinalTime(timer);

                Debug.Log("Level Complete");
            }
        }

        private void SaveFinalTime(float time)
        {
            // Load the old time
            float loadedTime = LoadFinalTime();

            // Compare the current time with the loaded time
            if (time < loadedTime || loadedTime == 0f)
            {
                // If the new time is faster or there's no loaded time, update PlayerPrefs
                PlayerPrefs.SetFloat(FinalTimeKey, time);
                PlayerPrefs.Save();
            }
        }

        private float LoadFinalTime()
        {
            return PlayerPrefs.GetFloat(FinalTimeKey, 0f);
        }

        private void UpdateSavedTimeText()
        {
            float savedTime = LoadFinalTime();
            int savedMinutes = Mathf.FloorToInt(savedTime / 60);
            int savedSeconds = Mathf.FloorToInt(savedTime % 60);
            int savedMilliseconds = Mathf.FloorToInt((savedTime * 1000) % 1000);

            savedTimeText.text = string.Format("Saved Time: {0:00}:{1:00}:{2:000}", savedMinutes, savedSeconds, savedMilliseconds);
        }

        private IEnumerator WinRoutine()
        {
            // Play transition and open win screen
            TransitionFader.PlayTransition(endTransitionPrefab);

            float fadeDelay = (endTransitionPrefab != null) ? endTransitionPrefab.Delay + endTransitionPrefab.FadeOnDuration : 0f;
            yield return new WaitForSeconds(fadeDelay);

            WinScreen winScreenPrefab = MenuManager.Instance.winScreenPrefab;

            if (winScreenPrefab != null)
            {
                WinScreen instantiatedWinScreen = Instantiate(winScreenPrefab);

                // Pass the current race time to the WinScreen
                instantiatedWinScreen.UpdateTimerText(timer);

                // Open the WinScreen using MenuManager
                MenuManager.Instance.OpenMenu(instantiatedWinScreen);
            }
        }

        private void Update()
        {
            if (objective != null && objective.IsComplete && isCountdownFinished)
            {
                EndLevel();
            }
        }
    }
}
