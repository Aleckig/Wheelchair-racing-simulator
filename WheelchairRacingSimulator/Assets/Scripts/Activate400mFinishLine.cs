using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate400mFinishLine : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";
    public GameObject objectB; // The object to teleport
    public GameObject teleportTarget; // The empty GameObject where you want to teleport the object

    
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == playerTag)
            {
                // Teleport object B to the teleport target
                Debug.Log("Teleporting object B to the teleport target");
                objectB.transform.position = teleportTarget.transform.position;
            }
        }
       
    
}
