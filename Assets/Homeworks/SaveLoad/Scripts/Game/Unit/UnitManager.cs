using System.Collections.Generic;
using Sirenix.Utilities;

namespace Homeworks.SaveLoad
{
    public class UnitManager
    {
        private readonly List<UnitObject> units = new();

        public UnitManager(UnitObject[] units)
        {
            this.units.AddRange(units);
        }

        public List<UnitObject> GetUnits()
        {
            return units;
        }
    }
}