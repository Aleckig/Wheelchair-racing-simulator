using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WheelchairGame;   

namespace WheelchairGame
{
    public class BestTimeManager100M : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI bestTimesText;
        [SerializeField] private int maxBestTimes = 5;

        private List<float> bestTimesList = new List<float>();

        private void Start()
        {
            // Load best times from PlayerPrefs on startup
            LoadBestTimes();
        }

        public void AddTime(float time)
        {
            // Add the time to the list
            bestTimesList.Add(time);

            // Sort the list in ascending order
            bestTimesList.Sort();

            // Keep only the top maxBestTimes times in the list
            while (bestTimesList.Count > maxBestTimes)
            {
                bestTimesList.RemoveAt(0); // Remove the lowest time
            }

            // Save best times to PlayerPrefs
            SaveBestTimes();

            // Update UI
            UpdateBestTimesUI();
        }

        private void UpdateBestTimesUI()
        {
            // Update the UI with the best times
            string timesText = "Best Times:\n";
            for (int i = 0; i < bestTimesList.Count; i++)
            {
                int minutes = Mathf.FloorToInt(bestTimesList[i] / 60);
                int seconds = Mathf.FloorToInt(bestTimesList[i] % 60);
                int milliseconds = Mathf.FloorToInt((bestTimesList[i] * 1000) % 1000);

                timesText += $"{i + 1}. {minutes:00}:{seconds:00}:{milliseconds:000}\n";
            }

            bestTimesText.text = timesText;
        }

        private void SaveBestTimes()
        {
            // Convert the list to a comma-separated string and save to PlayerPrefs
            string timesString = string.Join(",", bestTimesList.ConvertAll(x => x.ToString()).ToArray());
            PlayerPrefs.SetString("BestTimes", timesString);
        }

        private void LoadBestTimes()
        {
            // Load the string from PlayerPrefs and convert it back to a list
            string timesString = PlayerPrefs.GetString("BestTimes", "");
            string[] timesArray = timesString.Split(',');

            bestTimesList.Clear();

            foreach (string time in timesArray)
            {
                if (float.TryParse(time, out float parsedTime))
                {
                    bestTimesList.Add(parsedTime);
                }
            }

            // Ensure the loaded list is sorted
            bestTimesList.Sort();
        }
    }
            

}


    

