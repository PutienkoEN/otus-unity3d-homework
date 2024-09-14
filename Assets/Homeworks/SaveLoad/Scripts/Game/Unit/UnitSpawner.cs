using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitSpawner
    {
        private readonly Transform unitContainer;
        private readonly Dictionary<string, UnitObject> unitPrefabs;

        [Inject]
        public UnitSpawner(Transform unitContainer, List<UnitObject> unitPrefabs)
        {
            this.unitContainer = unitContainer;
            this.unitPrefabs = unitPrefabs
                .ToDictionary(unit => unit.unitTypeUid, unit => unit);
        }

        public UnitObject SpawnUnit(UnitCreateCommand unitCreateCommand)
        {
            var unitUnitTypeUid = unitCreateCommand.UnitTypeUid;
            if (!unitPrefabs.TryGetValue(unitUnitTypeUid, out UnitObject unitPrefab))
            {
                throw new KeyNotFoundException($"There is no unit prefab for uid {unitCreateCommand.UnitTypeUid}");
            }

            var unit = Object.Instantiate(unitPrefab, unitContainer, true);

            return UnitSetup(unit, unitCreateCommand);
        }

        private static UnitObject UnitSetup(UnitObject unit, UnitCreateCommand unitCreateCommand)
        {
            unit.unitTypeUid = unitCreateCommand.UnitTypeUid;
            unit.hitPoints = unitCreateCommand.HitPoints;
            unit.speed = unitCreateCommand.Speed;
            unit.damage = unitCreateCommand.Damage;

            var unitObjectTransform = unit.transform;
            unitObjectTransform.position = unitCreateCommand.Position;
            unitObjectTransform.rotation = unitCreateCommand.Rotation;
            unitObjectTransform.localScale = unitCreateCommand.Scale;

            return unit;
        }
    }
}