using System.Collections;
using System.Collections.Generic;

namespace LevelManagement
{
    public class SaveData
    {
        public string playerName;
        private readonly string defaultName = "Player";

        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;

        //public int highScore;
        //public int currentLevel;
        //public int unlockedLevel;

        public SaveData()
        {
            playerName = defaultName;
            masterVolume = 0.5f;
            musicVolume = 0.5f;
            sfxVolume = 0.5f;
            //highScore = 0;
            //currentLevel = 0;
            //unlockedLevel = 0;
        }
    }
}
