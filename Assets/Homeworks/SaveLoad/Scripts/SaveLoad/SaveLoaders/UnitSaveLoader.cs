using System;
using System.Collections.Generic;
using Homeworks.SaveLoad.Data;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitSaveLoader : AbstractSaveLoader<UnitSaveLoader.UnitDataStorage>
    {
        private readonly UnitManager unitManager;

        [Inject]
        public UnitSaveLoader(
            MyGameRepository gameRepository,
            UnitManager unitManager) : base(gameRepository)
        {
            this.unitManager = unitManager;
        }

        protected override UnitDataStorage GetDataToSave()
        {
            var units = unitManager.GetUnits();
            var unitData = units.ConvertAll(ConvertUnitToData);

            return new UnitDataStorage { Units = unitData };
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

        protected override void HandleDataLoad(UnitDataStorage savedData)
        {
            savedData.Units
                .ConvertAll(ConvertDataToUnit)
                .ForEach(unitManager.CreateUnit);
        }

        private UnitCreateCommand ConvertDataToUnit(UnitSaveData unitSaveData)
        {
            var transformData = unitSaveData.TransformData;
            return new UnitCreateCommand(
                unitSaveData.UnitTypeUid,
                unitSaveData.HitPoints,
                unitSaveData.Damage,
                unitSaveData.Speed,
                transformData.GetPositionAsVector3(),
                transformData.GetRotationAsQuaternion(),
                transformData.GetScaleAsVector3()
            );
        }

        [Serializable]
        public class UnitDataStorage
        {
            public List<UnitSaveData> Units { get; set; }
        }

        [Serializable]
        public class UnitSaveData
        {
            public string UnitTypeUid { get; set; }
            public int HitPoints { get; set; }
            public int Speed { get; set; }
            public int Damage { get; set; }
            public TransformData TransformData { get; set; }
        }
    }
}