using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
using LevelManagement;

namespace WheelchairGame
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private Objective objective;

        private bool isGameOver;

        private static GameManager instance;

        public static GameManager Instance { get { return instance; } }

        [SerializeField] int MainMenuIndex = 0;

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

        public void EndLevel()
        {
            if (!isGameOver)
            {
                isGameOver = true;
                LoadNextLevel();
                Debug.Log("Level Complete");
            }
        }
        private void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if(levelIndex == MainMenuIndex )
                {
                    MainMenu.Open();
                }
                SceneManager.LoadScene(levelIndex);
                
                
            }
            else
            {
                Debug.LogError("Invalid scene index.");
            }
        }
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void Update()
        {
            if (objective != null)
            {
                if (objective.IsComplete)
                {
                    EndLevel();
                }
            }
            else
            {
                //Debug.LogError("Objective is null. Make sure the Objective script is attached to the appropriate object in the scene.");
            }
        }

        
    }

}

