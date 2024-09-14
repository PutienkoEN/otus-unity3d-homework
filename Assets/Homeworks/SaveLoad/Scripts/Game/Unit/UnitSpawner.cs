using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class UnitSpawner
    {
        private readonly GameObject unitPrefab;

        public UnitSpawner(GameObject unitPrefab)
        {
            this.unitPrefab = unitPrefab;
        }

        public UnitObject SpawnUnit(UnitCreateCommand unitCreateCommand)
        {
            var unit = Object.Instantiate(unitPrefab);
            var unitObject = unit.AddComponent<UnitObject>();

            unitObject.hitPoints = unitCreateCommand.HitPoints;
            unitObject.speed = unitCreateCommand.Speed;
            unitObject.damage = unitCreateCommand.Damage;
            var unitObjectTransform = unitObject.transform;

            unitObjectTransform.position = unitCreateCommand.Position;
            unitObjectTransform.rotation = unitCreateCommand.Rotation;
            unitObjectTransform.localScale = unitCreateCommand.Scale;

            return unitObject;
        }
    }
}