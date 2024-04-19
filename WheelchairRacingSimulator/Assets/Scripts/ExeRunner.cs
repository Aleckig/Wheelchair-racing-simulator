using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class ExeRunner : MonoBehaviour
{
    private Process BleakExe;


    public void Awake()
    {
        UnityEngine.Debug.Log("EXE");   
    }
    public void Start()
    {
        RunExe();
    }

    private void OnDestroy()
    {
        UnityEngine.Debug.Log("Killing EXE");
        KillExe();
    }


    public void RunExe()
    {
        if (BleakExe != null)
        {
            UnityEngine.Debug.Log("Exe already running");
            return;
        }
        else
        {
            BleakExe = new Process();
            BleakExe.StartInfo.FileName = "sensor.exe";
            BleakExe.StartInfo.WorkingDirectory = Application.dataPath;
            BleakExe.Start();
        }
    }
    public void KillExe()
    {
        UnityEngine.Debug.Log("EXE KILLED");
        BleakExe?.Kill();
    }
}
