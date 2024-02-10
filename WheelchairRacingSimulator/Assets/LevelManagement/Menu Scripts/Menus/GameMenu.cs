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
            // Check if the "P" key is pressed
            if (Input.GetKeyDown(KeyCode.P))
            {
                OnPausePressed();
            }
        }
        public void OnPausePressed()
        {
            Time.timeScale = 0;
            if (MenuManager.Instance != null && PauseMenu.Instance != null)
            {
                MenuManager.Instance.OpenMenu(PauseMenu.Instance);
            }
            else
            {
                Debug.LogError("Menu Manager or Pause Menu not found in the scene.");
            }
        }
    }
}

