using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class CreateResourceCommand
    {
        public string ResourceTypeUid { get; }
        public ResourceType ResourceType { get; }
        public int RemainingCount { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }

        public CreateResourceCommand(
            string resourceTypeUid,
            ResourceType resourceType,
            int remainingCount,
            Vector3 position,
            Quaternion rotation,
            Vector3 scale)
        {
            ResourceTypeUid = resourceTypeUid;
            ResourceType = resourceType;
            RemainingCount = remainingCount;
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}