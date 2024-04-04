using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonSpeedReader : MonoBehaviour
{
    string jsonFilePath = @"C:\json\speedData.json";

    void Update()
    {
        // Read the JSON file
        string jsonContent = File.ReadAllText(jsonFilePath);

        // Parse the JSON data
        SpeedData speedData = JsonUtility.FromJson<SpeedData>(jsonContent);

        // Access speed data
        Debug.Log("Speed: " + speedData.speed);


    }
}

[System.Serializable]
public class SpeedData
{
    public float speed;
}
