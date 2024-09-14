using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class UnitSpawner
    {
        private readonly UnitObject unitPrefab;

        public UnitSpawner(UnitObject unitPrefab)
        {
            this.unitPrefab = unitPrefab;
        }

        public UnitObject SpawnUnit(UnitCreateCommand unitCreateCommand)
        {
            var unit = Object.Instantiate(unitPrefab);

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