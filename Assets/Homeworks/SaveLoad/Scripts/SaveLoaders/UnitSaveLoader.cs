using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class UnitSaveLoader : ISaveLoader
    {
        public void LoadGame()
        {
            Debug.Log("Unit loaded");
        }

        public void SaveGame()
        {
            Debug.Log("Unit saved");
        }
    }
}