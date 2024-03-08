using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class HighScoreManager : MonoBehaviour
    {
         private const string HIGHSCORE_100M_KEY = "Highscore_100m";
        private const string HIGHSCORE_400M_KEY = "Highscore_400m";

        // Set initial highscores if they don't exist
        void Start()
        {
            if (!PlayerPrefs.HasKey(HIGHSCORE_100M_KEY))
                PlayerPrefs.SetFloat(HIGHSCORE_100M_KEY, float.MaxValue);

            if (!PlayerPrefs.HasKey(HIGHSCORE_400M_KEY))
                PlayerPrefs.SetFloat(HIGHSCORE_400M_KEY, float.MaxValue);
        }

        // Update highscores if a new record is achieved
        public void UpdateHighscore(float time, string level)
        {
            string key = GetHighscoreKey(level);

            float currentHighscore = PlayerPrefs.GetFloat(key, float.MaxValue);

            if (time < currentHighscore)
            {
                PlayerPrefs.SetFloat(key, time);
            }
        }

        // Get highscore for a specific level
        public float GetHighscore(string level)
        {
            string key = GetHighscoreKey(level);
            return PlayerPrefs.GetFloat(key, float.MaxValue);
        }

        // Helper method to get the appropriate key based on the level
        private string GetHighscoreKey(string level)
        {
            switch (level)
            {
                case "100m":
                    return HIGHSCORE_100M_KEY;
                case "400m":
                    return HIGHSCORE_400M_KEY;
                // Add more cases if you have additional levels
                default:
                    Debug.LogWarning($"Unexpected level: {level}. Defaulting to 100m.");
                    return HIGHSCORE_100M_KEY;
            }
        }

        public List<float> GetTopScores(string level, int count = 10)
        {
            string key = GetHighscoreKey(level);
            List<float> topScores = new List<float>();

            for (int i = 1; i <= count; i++)
            {
                float score = PlayerPrefs.GetFloat($"{key}_{i}", float.MaxValue);
                if (score < float.MaxValue)
                {
                    topScores.Add(score);
                }
            }

            return topScores;
        }
    }
    
}

