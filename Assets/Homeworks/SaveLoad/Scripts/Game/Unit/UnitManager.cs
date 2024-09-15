using System.Collections.Generic;
using System.Linq;
using Zenject;
using Object = UnityEngine.Object;

namespace Homeworks.SaveLoad
{
    public class UnitManager
    {
        private readonly UnitSpawner unitSpawner;
        private readonly List<UnitObject> units;

        [Inject]
        public UnitManager(UnitSpawner unitSpawner, UnitObject[] units)
        {
            this.unitSpawner = unitSpawner;

            this.units = units.ToList();
            this.units
                .ForEach(unit => unit.OnViewDestroy += OnViewDestroy);
        }

        private void OnViewDestroy(UnitObject unit)
        {
            units.Remove(unit);
            unit.OnViewDestroy -= OnViewDestroy;
        }

        public List<UnitObject> GetUnits()
        {
            return units;
        }

        public void CreateUnit(UnitCreateCommand unitCreateCommand)
        {
            var unit = unitSpawner.SpawnUnit(unitCreateCommand);
            unit.OnViewDestroy += OnViewDestroy;
            units.Add(unit);
        }

        public void RemoveUnitsFromScene()
        {
            units
                .ConvertAll(resource => resource.gameObject)
                .ForEach(Object.Destroy);
        }
    }
}