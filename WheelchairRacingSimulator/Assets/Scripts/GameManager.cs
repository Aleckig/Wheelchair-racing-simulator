using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // reference to player
        [SerializeField] private GameObject player;


        // reference to player
        private Objective objective;

        private bool isGameOver;
        public bool IsGameOver { get { return isGameOver; } }


        // initialize references
        private void Awake()
        {
            
            objective = Object.FindObjectOfType<Objective>();
            
        }

        // end the level
        public void EndLevel()
        {
           
            // check if we have set IsGameOver to true, only run this logic once
            if (isGameOver)
            {
                isGameOver = true;
                
            }
        }

        // check for the end game condition on each frame
        private void Update()
        {
            if (objective != null & objective.IsComplete)
            {
                EndLevel();
            }
        }

    
}
