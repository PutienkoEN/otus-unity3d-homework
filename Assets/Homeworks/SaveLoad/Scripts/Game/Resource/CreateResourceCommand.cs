namespace Homeworks.SaveLoad
{
    public class CreateResourceCommand
    {
        public string ResourceTypeUid { get; }
        public ResourceType ResourceType { get; }
        public int RemainingCount { get; }

        public CreateResourceCommand(
            string resourceTypeUid,
            ResourceType resourceType,
            int remainingCount)
        {
            ResourceTypeUid = resourceTypeUid;
            ResourceType = resourceType;
            RemainingCount = remainingCount;
        }
    }
}