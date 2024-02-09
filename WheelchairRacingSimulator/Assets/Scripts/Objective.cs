using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    // tag to identify the player
        [SerializeField]
        private string playerTag = "Player";

        // is the objective complete?
        private bool isComplete;
        public bool IsComplete { get { return isComplete; } }

        // set the objective to complete
        public void CompleteObjective()
        {
            isComplete = true;
        }

        // when the player touches the trigger...
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == playerTag)
            {
                CompleteObjective();
            }
        }
}
