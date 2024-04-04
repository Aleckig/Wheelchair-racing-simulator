using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SpeedUpdater : MonoBehaviour
{
    public string jsonFilePath = @"C:\json\speedData.json"; // Path to your JSON file
    public float updateInterval = 1f; // Interval in seconds to check for updates
    public float speedTest;
    private Coroutine dataUpdateCoroutine;

    void Start()
    {
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
            speedTest = speedData.speed;
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
