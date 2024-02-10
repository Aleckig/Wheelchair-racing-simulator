using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        // Update is called once per frame
        void Update()
        {
            // Check if the "Esc" key is pressed
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPausePressed();
            }
        }
        public void OnPausePressed()
        {
            Time.timeScale = 0;
            PauseMenu.Open();
        }
    }
}

