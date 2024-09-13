using System.Collections.Generic;
using Sirenix.Utilities;

namespace Homeworks.SaveLoad
{
    public class UnitManager
    {
        private readonly HashSet<UnitObject> units = new();

        public UnitManager(UnitObject[] units)
        {
            this.units.AddRange(units);
        }

        public HashSet<UnitObject> GetUnits()
        {
            return units;
        }
    }
}