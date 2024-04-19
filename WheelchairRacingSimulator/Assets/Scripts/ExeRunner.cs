using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ExeRunner : MonoBehaviour
{
    private Process BleakExe;

    public void Start()
    {
        BleakExe = new Process();
        // Create a Process object
        //Process process = new Process();

        // Set StartInfo properties
        //process.StartInfo.FileName = "testbleak2.exe";
        //process.StartInfo.WorkingDirectory = Application.dataPath; // Assuming the exe is in the data path (game root)

        // Optionally hide the console window (Windows only)
        //process.StartInfo.CreateNoWindow = true;

        // Start the process
        //process.Start();
        
    }


    public void RunExe()
    {
        BleakExe.StartInfo.FileName = "testbleak2.exe";
        BleakExe.StartInfo.WorkingDirectory = Application.dataPath;
        BleakExe.Start();
       //Process process = new Process();
       //process.StartInfo.FileName = "testbleak2.exe";
       //process.StartInfo.WorkingDirectory = Application.dataPath; // Assuming the exe is in the data path (game root)
    }

    public void KillExe()
    {
        BleakExe?.Kill();
    }
}
