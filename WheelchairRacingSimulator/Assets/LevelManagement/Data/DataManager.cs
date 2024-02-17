using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private SaveData saveData;
        private JsonSaver jsonSaver;
        public float MasterVolume { get { return saveData.masterVolume; } set { saveData.masterVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }

        private void Awake()
        {
            saveData = new SaveData();
            jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            jsonSaver.Save(saveData);
        }

        public void Load()
        {
            jsonSaver.Load(saveData);
           
        }

        
        
    }
}