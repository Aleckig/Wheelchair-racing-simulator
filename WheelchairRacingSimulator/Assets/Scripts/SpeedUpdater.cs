using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SpeedUpdater : MonoBehaviour
{
    private string jsonFilePath;
    public string jsonFileName = "speed.json";
    public float updateInterval = 1f; // Interval in seconds to check for updates
    public float speed;
    public float SpeedMultiplier = 3.6f;
    private Coroutine dataUpdateCoroutine;

    void Start()
    {
        //Finds the json file in the game build data directory 
        jsonFilePath = Path.Combine(Application.dataPath, jsonFileName);

        dataUpdateCoroutine = StartCoroutine(UpdateDataCoroutine());
    }

    IEnumerator UpdateDataCoroutine()
    {
        while (true)
        {
            // Read the JSON file
            string jsonContent = File.ReadAllText(jsonFilePath);

            // Parse the JSON data
            SpeedData speedData = JsonUtility.FromJson<SpeedData>(jsonContent);

            // Access speed data
            Debug.Log("Speed: " + speedData.speed);

            // Get speed from json and multiply to get m/s
            speed = speedData.speed * SpeedMultiplier;
            

            // Wait for the specified interval before checking for updates again
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void OnDestroy()
    {
        // Stop the coroutine when the object is destroyed
        if (dataUpdateCoroutine != null)
            StopCoroutine(dataUpdateCoroutine);
    }


    [System.Serializable]
    public class SpeedData
    {
        public float speed;
    }
}
