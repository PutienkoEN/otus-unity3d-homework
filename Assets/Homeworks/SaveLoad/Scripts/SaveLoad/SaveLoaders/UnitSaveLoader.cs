using System;
using System.Collections.Generic;
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
            gameRepository.SetData(unitData);
            Debug.Log($"Units saved, amount of saved {units.Count}");
        }

        public void LoadData()
        {
            if (gameRepository.TryGetData<List<UnitSaveData>>(out var unitsData))
            {
                unitsData.ForEach(LoadUnit);
                Debug.Log($"Units loaded, amount of loaded {unitsData.Count}");
            }
        }

        private void LoadUnit(UnitSaveData unitSaveData)
        {
            var unitSpawnCommand = new UnitCreateCommand(
                unitSaveData.UnitTypeUid,
                unitSaveData.HitPoints,
                unitSaveData.Damage,
                unitSaveData.Speed,
                Vector3FromArray(unitSaveData.Position),
                QuaternionFromArray(unitSaveData.Rotation),
                Vector3FromArray(unitSaveData.Scale)
            );

            unitManager.CreateUnit(unitSpawnCommand);
        }

        private static UnitSaveData ConvertUnitToData(UnitObject unit)
        {
            var unitTransform = unit.transform;

            return new UnitSaveData(
                unitTypeUid: unit.unitTypeUid,
                hitPoints: unit.hitPoints,
                damage: unit.damage,
                speed: unit.speed,
                position: Vector3ToArray(unitTransform.position),
                rotation: QuaternionToArray(unitTransform.rotation),
                scale: Vector3ToArray(unitTransform.localScale));
        }

        private static float[] Vector3ToArray(Vector3 gameObjectPosition)
        {
            var position = new float[3];

            position[0] = gameObjectPosition.x;
            position[1] = gameObjectPosition.y;
            position[2] = gameObjectPosition.z;

            return position;
        }

        private static Vector3 Vector3FromArray(float[] position)
        {
            return new Vector3(position[0], position[1], position[2]);
        }

        private static float[] QuaternionToArray(Quaternion gameObjectRotation)
        {
            var rotation = new float[4];

            rotation[0] = gameObjectRotation.x;
            rotation[1] = gameObjectRotation.y;
            rotation[2] = gameObjectRotation.z;
            rotation[3] = gameObjectRotation.w;

            return rotation;
        }

        private static Quaternion QuaternionFromArray(float[] rotation)
        {
            return new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
        }

        [Serializable]
        public class UnitSaveData
        {
            public string UnitTypeUid { get; }
            public int HitPoints { get; }
            public int Speed { get; }
            public int Damage { get; }
            public float[] Position { get; }
            public float[] Rotation { get; }
            public float[] Scale { get; }

            public UnitSaveData(
                string unitTypeUid,
                int hitPoints,
                int speed,
                int damage,
                float[] position,
                float[] rotation,
                float[] scale)
            {
                UnitTypeUid = unitTypeUid;
                HitPoints = hitPoints;
                Speed = speed;
                Damage = damage;
                Position = position;
                Rotation = rotation;
                Scale = scale;
            }
        }
    }
}