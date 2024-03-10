using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsViewer : MonoBehaviour
{
    
        void Start()
        {
            Debug.Log("PlayerPrefs keys:");
            
            // Iterate through all potential keys
            for (int i = 0; i < PlayerPrefs.GetInt("PlayerPrefsKeysCount"); i++)
            {
                string key = PlayerPrefs.GetString("PlayerPrefsKey_" + i);

                if (PlayerPrefs.HasKey(key))
                {
                    Debug.Log(key + ": " + PlayerPrefs.GetString(key));
                }
            }
        }
    
}
