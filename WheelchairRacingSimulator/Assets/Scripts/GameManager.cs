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
                WinScreen.Open();
                Debug.Log("Level Complete");
            }
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

