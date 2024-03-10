using System.Collections;
using System.Collections.Generic;
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
        public float timer;
        public float Timer => timer;
        [SerializeField] private TestMovement testMovement;
        public float CurrentTimer => timer;

        [SerializeField] private int maxBestTimes = 5;
        private List<float> bestTimes;
        [SerializeField] private TextMeshProUGUI bestTimesText;

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

            //bestTimes = new List<float>() { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
            bestTimes = new List<float>() { 999f, 999f, 999f, 999f, 999f };

            LoadBestTimes();
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
            LoadBestTimes();
            timer = 0f;
            StartCoroutine(CountdownRoutine());
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

        private bool shouldUpdateBestTimes = false; // New flag

        private IEnumerator TimerRoutine()
        {
            Debug.Log("TimerRoutine started");
            while (!isGameOver)
            {
                if (isCountdownFinished)
                {
                    timer += Time.deltaTime;
                    UpdateTimerText();

                    CheckForBestTime();

                    // Set the flag to indicate that best times should be updated
                    shouldUpdateBestTimes = true;
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
        }

        private void LoadBestTimes()
        {
            for (int i = 0; i < maxBestTimes; i++)
            {
                string key = "BestTime_" + i;
                float bestTime = PlayerPrefs.GetFloat(key, float.MaxValue);
                bestTimes[i] = bestTime;
            }
        }

        private void SaveBestTimes()
        {
            Debug.Log("Save started");
            for (int i = 0; i < maxBestTimes; i++)
            {
                string key = "BestTime_" + i;
                PlayerPrefs.SetFloat(key, bestTimes[i]);
            }
        }
        //private bool displayBestTimes = false; 

        private void UpdateBestTimesText()
        {
            Debug.Log("UpdateBestTimesText called");
            Debug.Log("Best Times Count: " + bestTimes.Count);
            // Update the UI with the best times only after the race is finished
            if (isGameOver)
            {
                Debug.Log("Entering UpdateBestTimesText loop");
                bestTimesText.text = "Best Times:\n";

                for (int i = 0; i < bestTimes.Count && i < maxBestTimes; i++)
                {
                    int minutes = Mathf.FloorToInt(bestTimes[i] / 60);
                    int seconds = Mathf.FloorToInt(bestTimes[i] % 60);
                    int milliseconds = Mathf.FloorToInt((bestTimes[i] * 1000) % 1000);

                    bestTimesText.text += string.Format("{0}. {1:00}:{2:00}:{3:000}\n", i + 1, minutes, seconds, milliseconds);
                }
            }
        }

        private void CheckForBestTime()
        {
            if (bestTimes.Count > 0)
            {
                if (timer < bestTimes[bestTimes.Count - 1])
                {
                    bestTimes.Add(timer);
                    bestTimes.Sort();

                    if (bestTimes.Count > maxBestTimes)
                    {
                        bestTimes.RemoveAt(maxBestTimes);
                    }

                    //SaveBestTimes();
                    UpdateBestTimesText();
                }
            }
        }
        private void EndLevel()
        {
            // End the level
            if (!isGameOver)
            {
                isGameOver = true;
                StopAllCoroutines(); // Stop the countdown and timer routines
                SaveBestTimes();
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

            // Access the WinScreen prefab from the MenuManager
            WinScreen winScreenPrefab = MenuManager.Instance.winScreenPrefab;

            if (winScreenPrefab != null)
            {
                // Instantiate the WinScreen prefab
                WinScreen instantiatedWinScreen = Instantiate(winScreenPrefab);

                // Log statements for debugging
                Debug.Log("GameManager.Instance: " + (GameManager.Instance != null));
                Debug.Log("GameManager Timer: " + (GameManager.Instance != null ? GameManager.Instance.Timer.ToString() : "GameManager.Instance is null"));

                // Call the UpdateTimerText method with the current timer value from GameManager
                instantiatedWinScreen.UpdateTimerText(GameManager.Instance.Timer);

                // Open the WinScreen using MenuManager
                MenuManager.Instance.OpenMenu(instantiatedWinScreen);
            }
        }

        private void Update()
        {
            // Check if the objective is complete and the countdown has finished
            if (objective != null && objective.IsComplete && isCountdownFinished)
            {
                EndLevel();
            }

            if (!isGameOver)
            {
                UpdateTimerText();

                // Check if the flag is set to update best times
                if (shouldUpdateBestTimes)
                {
                    UpdateBestTimesText();
                    shouldUpdateBestTimes = false; // Reset the flag
                }
            }
        }
    }
        /*
        
        
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
        public float timer;
        public float Timer => timer;
        [SerializeField] private TestMovement testMovement;
        public float CurrentTimer => timer;

        /// 
         // New variables for best times
        [SerializeField] private int maxBestTimes = 5; // Maximum number of best times to store
        private List<float> bestTimes;
        [SerializeField] private TextMeshProUGUI bestTimesText;
        
        /// 

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

            ///
            // Change this line in the Awake method
            //bestTimes = new List<float>();
            //bestTimes = new List<float>() { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
            bestTimes = new List<float>() { 999999f, 999999f, 999999f, 999999f, 999999f };

            LoadBestTimes();
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
            //LoadBestTimes();
            timer = 0f;
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
                CheckForBestTime();

                UpdateBestTimesText();  

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

        private void LoadBestTimes()
        {
            Debug.Log("Loading best times...");

            // Load best times from PlayerPrefs
            string sceneName = SceneManager.GetActiveScene().name;
            string key = "BestTimes_" + sceneName;

            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);

                // Log scene name
                Debug.Log("Scene name: " + sceneName);

                // Log raw JSON string
                Debug.Log("Raw JSON: " + json);

                try
                {
                    // Check if the JSON string is empty
                    if (!string.IsNullOrEmpty(json))
                    {
                        bestTimes = JsonUtility.FromJson<List<float>>(json);
                        Debug.Log("Loaded bestTimes: " + string.Join(", ", bestTimes));
                    }
                    else
                    {
                        // Initialize an empty list if JSON is empty
                        bestTimes = new List<float>();
                        Debug.Log("No saved best times found.");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to parse bestTimes JSON: " + e.Message);
                }
            }
            else
            {
                // If no saved best times, initialize an empty list
                bestTimes = new List<float>();
                Debug.Log("No saved best times found.");
            }
        }

        private void SaveBestTimes()
        {
            Debug.Log("SaveRoutine");

            try
            {
                // Save best times to PlayerPrefs
                string sceneName = SceneManager.GetActiveScene().name;
                string key = "BestTimes_" + sceneName;

                string json = JsonUtility.ToJson(bestTimes);
                PlayerPrefs.SetString(key, json);

                Debug.Log("Saved bestTimes: " + string.Join(", ", bestTimes));
                Debug.Log("SaveRoutine success");
            }
            catch (System.Exception e)
            {
                Debug.LogError("SaveRoutine error: " + e.Message);
            }
        }

        private bool defaultsUpdated = false;

        private void UpdateBestTimesText()
        {
            /// Update the UI with the best times
            bestTimesText.text = "Best Times:\n";

            for (int i = 0; i < bestTimes.Count && i < maxBestTimes; i++)
            {
                // Update default values if they are still set to float.MaxValue and defaults haven't been updated
                if (!defaultsUpdated && bestTimes[i] == float.MaxValue)
                {
                    bestTimes[i] = timer; // Set to the current timer value
                }

                int minutes = Mathf.FloorToInt(bestTimes[i] / 60);
                int seconds = Mathf.FloorToInt(bestTimes[i] % 60);
                int milliseconds = Mathf.FloorToInt((bestTimes[i] * 1000) % 1000);

                bestTimesText.text += string.Format("{0}. {1:00}:{2:00}:{3:000}\n", i + 1, minutes, seconds, milliseconds);
            }

            // Set the defaultsUpdated flag to true after the first update
            defaultsUpdated = true;

            // Debug log to check if bestTimesText is being updated
            Debug.Log("Updated bestTimesText: " + bestTimesText.text);
            
        }
        private void CheckForBestTime()
        {
            // Ensure bestTimes list is not empty
            if (bestTimes.Count > 0)
            {
                // Check if the current time is a new best time
                if (timer < bestTimes[bestTimes.Count - 1])
                {
                    // Add the new best time
                    bestTimes.Add(timer);

                    // Sort the list
                    bestTimes.Sort();

                    // Remove the last element if the list exceeds maxBestTimes
                    if (bestTimes.Count > maxBestTimes)
                    {
                        bestTimes.RemoveAt(maxBestTimes);
                    }

                    // Save the updated list
                    SaveBestTimes();

                    // Update the UI
                    UpdateBestTimesText();
                }
            }
                    
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

                SaveBestTimes();
            }
        }

        

        private IEnumerator WinRoutine()
        {
            // Play transition and open win screen
            TransitionFader.PlayTransition(endTransitionPrefab);

            float fadeDelay = (endTransitionPrefab != null) ? endTransitionPrefab.Delay + endTransitionPrefab.FadeOnDuration : 0f;
            yield return new WaitForSeconds(fadeDelay);

            // Access the WinScreen prefab from the MenuManager
            WinScreen winScreenPrefab = MenuManager.Instance.winScreenPrefab;

            if (winScreenPrefab != null)
            {
                // Instantiate the WinScreen prefab
                WinScreen instantiatedWinScreen = Instantiate(winScreenPrefab);

                // Log statements for debugging
                Debug.Log("GameManager.Instance: " + (GameManager.Instance != null));
                Debug.Log("GameManager Timer: " + (GameManager.Instance != null ? GameManager.Instance.Timer.ToString() : "GameManager.Instance is null"));

                // Call the UpdateTimerText method with the current timer value from GameManager
                instantiatedWinScreen.UpdateTimerText(GameManager.Instance.Timer);

                // Open the WinScreen using MenuManager
                MenuManager.Instance.OpenMenu(instantiatedWinScreen);
            }
            
        }
        

        private void Update()
        {
            // Check if the objective is complete and the countdown has finished
            if (objective != null && objective.IsComplete && isCountdownFinished)
            {
                EndLevel();
            }

            if (!isGameOver)
            {
                UpdateTimerText();
            }
        }
    }
*/
}

