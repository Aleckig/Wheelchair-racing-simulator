using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;
using TMPro;
using WheelchairGame;
using System.Collections.Generic;
using UnityEngine.UI;      

namespace WheelchairGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private Objective objective;
        private bool isGameOver;
        private static GameManager instance;

        [SerializeField] private TransitionFader endTransitionPrefab;
        [SerializeField] private float countdownDuration = 9f;
        [SerializeField] private TextMeshProUGUI countdownText;
        private bool isCountdownFinished = false;
        
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI savedTimeText; // UI element for saved time
        [SerializeField] private TextMeshProUGUI loadedTimeText;
        public bool IsCountdownFinished => isCountdownFinished;

        
        
        public float timer;
        public float Timer => timer;
       
        public float CurrentTimer => timer;

        private const int MaxSavedTimes = 5;

        private List<float> topTimes = new List<float>();

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
            
            StartCoroutine(CountdownRoutine());

            LoadTopTimes();
           
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
            LoadTopTimes();

            // Display loaded times
            loadedTimeText.text = "Top Times:\n";
            for (int i = 0; i < topTimes.Count; i++)
            {
                int topMinutes = Mathf.FloorToInt(topTimes[i] / 60);
                int topSeconds = Mathf.FloorToInt(topTimes[i] % 60);
                int topMilliseconds = Mathf.FloorToInt((topTimes[i] * 1000) % 1000);

                loadedTimeText.text += string.Format("{0}. {1:00}:{2:00}:{3:000}\n", i + 1, topMinutes, topSeconds, topMilliseconds);
            }
        }

        private void LoadTopTimes()
        {
            topTimes.Clear();
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i = 0; i < MaxSavedTimes; i++)
            {
                float time = PlayerPrefs.GetFloat($"{sceneName}_FinalTime_{i}", 0f);
                if (time > 0)
                {
                    topTimes.Add(time);
                }
            }
            topTimes.Sort();
        }

        private void SaveTopTimes()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            for (int i = 0; i < Mathf.Min(topTimes.Count, MaxSavedTimes); i++)
            {
                PlayerPrefs.SetFloat($"{sceneName}_FinalTime_{i}", topTimes[i]);
            }
            PlayerPrefs.Save();
        }

        public void EndLevel()
        {
            if (!isGameOver)
            {
                isGameOver = true;
                StopAllCoroutines();
                StartCoroutine(WinRoutine());

                AddTimeToTopTimes(timer);
                
                SaveTopTimes();

                Debug.Log("Level Complete");
            }
        }

        private void AddTimeToTopTimes(float time)
        {
            // If the list isn't full or the new time is faster than the slowest time in the list
            if (topTimes.Count < MaxSavedTimes || time < topTimes[topTimes.Count - 1])
            {
                if (topTimes.Count >= MaxSavedTimes)
                {
                    topTimes.RemoveAt(topTimes.Count - 1);
                }
                topTimes.Add(time);
                topTimes.Sort();
            }
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
