using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LevelManagement.Modes
{
    [Serializable]
    public class ModeSpecs 
    {
       [SerializeField] protected string name;
       [SerializeField] [Multiline] protected string description;
       [SerializeField] protected string sceneName;
       [SerializeField] protected string id;
       [SerializeField] protected Sprite image;
       [SerializeField] protected string bestTime;

       public string Name => name;
       public string Description => description;
       public string SceneName => sceneName;
       public string Id => id;
       public Sprite Image => image;
       public string BestTime => bestTime;




    }

}


