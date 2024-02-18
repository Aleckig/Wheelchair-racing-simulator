using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement.Modes
{
    public class ModeSelector : MonoBehaviour
    {
        [SerializeField] protected ModeList modeList;
        protected int currentIndex = 0;
        public int CurrentIndex => currentIndex;

        public void ClampIndex()
        {
            if(modeList.TotalModes == 0 )
            {
                Debug.LogError("No modes in the list");
                return;
            }
            if(currentIndex >= modeList.TotalModes)
            {
                currentIndex = 0;
            }
            if(currentIndex < 0)
            {
                currentIndex = modeList.TotalModes - 1;
            }
        }

        public void SetIndex(int index)
        {
            currentIndex = index;
            ClampIndex();
        }

        public void IncrementIndex()
        {
            SetIndex(currentIndex + 1);
        }

        public void DecrementIndex()
        {
            SetIndex(currentIndex - 1);
        }

        public ModeSpecs GetMode(int index)
        {
            return modeList.GetMode(index);
        }

        public ModeSpecs GetCurrentMode()
        {
            return modeList.GetMode(currentIndex);
        }  

    }

}
