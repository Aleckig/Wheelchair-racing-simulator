using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ExeRunner : MonoBehaviour
{
    public void Start()
    {
        // Create a Process object
        Process process = new Process();

        // Set StartInfo properties
        process.StartInfo.FileName = "testbleak2.exe";
        process.StartInfo.WorkingDirectory = Application.dataPath; // Assuming the exe is in the data path (game root)

        // Optionally hide the console window (Windows only)
        process.StartInfo.CreateNoWindow = true;

        // Start the process
        process.Start();
        
    }
}
