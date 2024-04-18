using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SpeedUpdater : MonoBehaviour
{
    //public string jsonFilePath = @"C:\Games\test\WheelchairRacingSimulator_Data\speed.json"; // Path to your JSON file
    public string jsonFilePath;
    public string jsonFileName = "speed.json";
    //public string jsonFilePath = Path.Combine(Application.persistentDataPath, "speed.json");
    public float updateInterval = 1f; // Interval in seconds to check for updates
    public float speed;
    private Coroutine dataUpdateCoroutine;

    void Start()
    {

        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);
        // Start coroutine to continuously update data
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
            speed = speedData.speed * 3.6f;
            

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
