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

        [SerializeField] private TransitionFader endTransitionPrefab;
        

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
                StartCoroutine(WinRoutine());
                Debug.Log("Level Complete");
            }
        }

        private IEnumerator WinRoutine()
        {
            
            TransitionFader.PlayTransition(endTransitionPrefab);
            
            float fadeDelay =(endTransitionPrefab != null) ? endTransitionPrefab.Delay + endTransitionPrefab.FadeOnDuration : 0f;
            yield return new WaitForSeconds(fadeDelay);
            WinScreen.Open();
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

