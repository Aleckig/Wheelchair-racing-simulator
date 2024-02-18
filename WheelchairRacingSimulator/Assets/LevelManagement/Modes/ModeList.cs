using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Modes
{
    [CreateAssetMenu(fileName = "ModeList", menuName = "Modes/Create ModeList", order = 1)]
    public class ModeList : ScriptableObject
    {
        [SerializeField] private List<ModeSpecs> modes;
        public int TotalModes => modes.Count;

        public ModeSpecs GetMode(int index)
        {
            if (index < 0 || index >= modes.Count)
            {
                return null;
            }
            return modes[index];
        }
    }
}

