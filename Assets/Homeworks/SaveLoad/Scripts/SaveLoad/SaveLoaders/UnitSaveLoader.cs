using System;
using System.Collections.Generic;
using Homeworks.SaveLoad.Data;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class UnitSaveLoader : ISaveLoader
    {
        private readonly UnitManager unitManager;
        private readonly MyGameRepository gameRepository;

        public UnitSaveLoader(UnitManager unitManager, MyGameRepository gameRepository)
        {
            this.unitManager = unitManager;
            this.gameRepository = gameRepository;
        }

        public void SaveData()
        {
            var units = unitManager.GetUnits();
            var unitData = units.ConvertAll(ConvertUnitToData);

            var unitDataStorage = new UnitDataStorage
            {
                Units = unitData
            };

            gameRepository.SetData(unitDataStorage);
            Debug.Log($"Units saved, amount of saved {units.Count}");
        }

        public void LoadData()
        {
            if (gameRepository.TryGetData<UnitDataStorage>(out var unitsData))
            {
                unitsData.Units.ForEach(LoadUnit);
                Debug.Log($"Units loaded, amount of loaded {unitsData.Units.Count}");
            }
        }

        private void LoadUnit(UnitSaveData unitSaveData)
        {
            var transformData = unitSaveData.TransformData;
            var unitSpawnCommand = new UnitCreateCommand(
                unitSaveData.UnitTypeUid,
                unitSaveData.HitPoints,
                unitSaveData.Damage,
                unitSaveData.Speed,
                transformData.GetPositionAsVector3(),
                transformData.GetRotationAsQuaternion(),
                transformData.GetScaleAsVector3()
            );

            unitManager.CreateUnit(unitSpawnCommand);
        }

        private static UnitSaveData ConvertUnitToData(UnitObject unit)
        {
            return new UnitSaveData
            {
                UnitTypeUid = unit.unitTypeUid,
                HitPoints = unit.hitPoints,
                Damage = unit.damage,
                Speed = unit.speed,
                TransformData = TransformData.FromTransform(unit.transform)
            };
        }

        [Serializable]
        private class UnitDataStorage
        {
            public List<UnitSaveData> Units { get; set; }
        }

        [Serializable]
        private class UnitSaveData
        {
            public string UnitTypeUid { get; set; }
            public int HitPoints { get; set; }
            public int Speed { get; set; }
            public int Damage { get; set; }
            public TransformData TransformData { get; set; }
        }
    }
}