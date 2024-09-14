using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class UnitCreateCommand
    {
        public string UnitTypeUid { get; }
        public int HitPoints { get; }
        public int Speed { get; }
        public int Damage { get; }

        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }

        public UnitCreateCommand(
            string unitTypeUid,
            int hitPoints,
            int speed,
            int damage,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale)
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