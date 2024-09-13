using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitSaveLoader : ISaveLoader
    {
        private UnitManager unitManager;
        private MyGameRepository gameRepository;

        [Inject]
        public UnitSaveLoader(UnitManager unitManager, MyGameRepository gameRepository)
        {
            this.unitManager = unitManager;
            this.gameRepository = gameRepository;
        }

        public void SaveGame()
        {
            Debug.Log("Unit saved");
            var units = unitManager.GetUnits();

            var unitData = units.ConvertAll(unit => new UnitData
            {
                HitPoints = unit.hitPoints,
                Damage = unit.damage,
                Speed = unit.speed
            });
            
            gameRepository.SetData(unitData);
        }

        public void LoadGame()
        {
            Debug.Log("Unit loaded");
        }

        class UnitData
        {
            public int HitPoints { get; set; }

            public int Speed { get; set; }

            public int Damage { get; set; }
        }
    }
}