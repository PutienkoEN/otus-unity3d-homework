namespace Homeworks.SaveLoad
{
    public class CreateResourceCommand
    {
        public ResourceType ResourceType { get; }
        public int RemainingCount { get; }

        public CreateResourceCommand(ResourceType resourceType, int remainingCount)
        {
            ResourceType = resourceType;
            RemainingCount = remainingCount;
        }
    }
}