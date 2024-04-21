using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class ExeRunner : MonoBehaviour
{
    private Process BleakExe;
    private string lastSceneName;

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
            RunOrKillExe();
        }
    }

    void RunOrKillExe()
    {
        if (lastSceneName == "MainMenu")
        {
            UnityEngine.Debug.Log("EXE Killed");
            KillExe();
        }
        else if (lastSceneName == "100M" || lastSceneName == "400M")
        {
            if (BleakExe == null || BleakExe.HasExited)
            {
                UnityEngine.Debug.Log("EXE Started");
                RunExe();
            }
        }
    }

    public void RunExe()
    {
        BleakExe = new Process();
        BleakExe.StartInfo.FileName = "sensor.exe";
        BleakExe.StartInfo.WorkingDirectory = Application.dataPath;
        BleakExe.Start();
        UnityEngine.Debug.Log("EXE Started");
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
