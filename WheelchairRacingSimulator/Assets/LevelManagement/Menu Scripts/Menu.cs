using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelchairGame;

namespace LevelManagement
{
    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        
        public virtual void OnBackPressed()
        {
            
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();

                Debug.Log("Back");
            }
            else
            {
                Debug.LogError("Menu Manager not found in the scene.");
            }
        }   
    }

}


