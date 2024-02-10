using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelchairGame;

namespace LevelManagement
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.LoadNextLevel();
            }
            else
            {
                Debug.LogError("Game Manager not found in the scene.");
            }

        }
        public void OnSettingsPressed()
        {
            Debug.Log("Settings");
        }
        public void OnCreditsPressed()
        {
            Debug.Log("Credits");
        }
        public void OnBackPressed()
        {
            Debug.Log("Back");
        }   
    }

}


