using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager instance; // Singleton instance
        private SaveData saveData;
        private JsonSaver jsonSaver;

        // Properties
        public float MasterVolume { get { return saveData.masterVolume; } set { saveData.masterVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }
        public string PlayerName { get { return saveData.playerName; } set { saveData.playerName = value; } }

        private void Awake()
        {
            // Singleton pattern
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
