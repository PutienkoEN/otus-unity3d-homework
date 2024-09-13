using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitSaveLoader : ISaveLoader
    {
        private UnitManager unitManager;

        [Inject]
        public UnitSaveLoader(UnitManager unitManager)
        {
            this.unitManager = unitManager;
        }

        public void SaveGame()
        {
            Debug.Log("Unit saved");

            unitManager.GetUnits()
                .ForEach(Debug.Log);
        }

        public void LoadGame()
        {
            Debug.Log("Unit loaded");
        }
    }
}