using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitSaveLoader : ISaveLoader
    {
        private readonly UnitManager unitManager;
        private readonly MyGameRepository gameRepository;

        [Inject]
        public UnitSaveLoader(UnitManager unitManager, MyGameRepository gameRepository)
        {
            this.unitManager = unitManager;
            this.gameRepository = gameRepository;
        }

        public void SaveData()
        {
            var units = unitManager.GetUnits();
            var unitData = units.ConvertAll(unit => new UnitData
            {
                HitPoints = unit.hitPoints,
                Damage = unit.damage,
                Speed = unit.speed
            });

            gameRepository.SetData(unitData);
            Debug.Log("Units saved");
        }

        public void LoadData()
        {
            if (gameRepository.TryGetData<List<UnitData>>(out var unitsData))
            {
                unitsData.ForEach(unitData => Debug.Log(unitData));
            }

            Debug.Log("Units loaded");
        }

        class UnitData
        {
            public int HitPoints { get; set; }

            public int Speed { get; set; }

            public int Damage { get; set; }
        }
    }
}