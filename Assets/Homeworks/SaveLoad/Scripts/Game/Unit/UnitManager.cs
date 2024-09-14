using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitManager
    {
        private readonly UnitSpawner unitSpawner;
        private readonly List<UnitObject> units;

        [Inject]
        public UnitManager(UnitSpawner unitSpawner, UnitObject[] units)
        {
            this.units = units.ToList();
            this.unitSpawner = unitSpawner;
        }

        public List<UnitObject> GetUnits()
        {
            return units;
        }

        public void CreateUnit(UnitCreateCommand unitCreateCommand)
        {
            var unit = unitSpawner.SpawnUnit(unitCreateCommand);
            units.Add(unit);
        }
    }
}