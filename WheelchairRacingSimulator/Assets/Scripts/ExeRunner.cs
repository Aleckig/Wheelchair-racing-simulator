using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class ExeRunner : MonoBehaviour
{
    private Process BleakExe;
    private string lastSceneName;
    private bool exeStarted;

    void Start()
    {
        // Register the scene loaded event handler
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        lastSceneName = SceneManager.GetActiveScene().name;
        RunOrKillExe();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        KillExe();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentSceneName = scene.name;
        if (lastSceneName != currentSceneName)
        {
            lastSceneName = currentSceneName;
            exeStarted = false; // Reset the flag when scene changes
            RunOrKillExe();
        }
    }

    void RunOrKillExe()
    {
        if (lastSceneName == "MainMenu")
        {
            UnityEngine.Debug.Log("EXE Killed");
            KillExe();
            exeStarted = false;
        }
        else if ((lastSceneName == "100M" || lastSceneName == "400M") && !exeStarted)
        {
            UnityEngine.Debug.Log("EXE Started");
            RunExe();
            exeStarted = true; // Set the flag indicating the executable has been started
        }
    }

    public void RunExe()
    {
        BleakExe = new Process();
        BleakExe.StartInfo.FileName = "sensor.exe";
        BleakExe.StartInfo.WorkingDirectory = Application.dataPath;
        BleakExe.Start();
    }

    public void KillExe()
    {
        if (BleakExe != null && !BleakExe.HasExited)
        {
            BleakExe.Kill();
            BleakExe.WaitForExit(); // Ensure the process is killed before proceeding
            UnityEngine.Debug.Log("EXE Killed");
        }
    }
}
