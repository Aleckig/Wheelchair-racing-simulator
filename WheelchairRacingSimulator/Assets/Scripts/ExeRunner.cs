using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class ExeRunner : MonoBehaviour
{
    private static ExeRunner instance;
    private Process BleakExe;
    public string applicationName = "sensor";

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of ExeRunner exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //we are not running exe on start because the sensor is so fragile and easily bugs out. Wheel needs to be turned after exe start to wake up the sensor. If its rotated before the start there is a possibility that the sensor freezes.
        //RunExe();
    }

    private void OnDestroy()
    {
        // On destroy makes sure that sensor exe doesn't stay on even if the game has been stopped
        KillExe();
    }
    public void RunExe()
    {
        
            // Runs sensor.exe in game data path. sensor.exe needs to be copied there after build to get it working. sensor.exe will create speed.json when launched the first time.
            BleakExe = new Process();
            BleakExe.StartInfo.FileName = "sensor.exe";
            BleakExe.StartInfo.WorkingDirectory = Application.dataPath;
            BleakExe.Start();
     
    }
    public void KillExe()
    {
        //Find all sensor processes and kill them
        Process[] processes = Process.GetProcessesByName(applicationName);

        foreach (Process process in processes)
        {
            // Close the process
            process.Kill();
        }        
    }
}
