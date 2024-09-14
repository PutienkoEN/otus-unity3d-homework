using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class ResourceSaveLoader : ISaveLoader
    {
        private readonly ResourceManager resourceManager;
        private readonly MyGameRepository gameRepository;

        [Inject]
        public ResourceSaveLoader(ResourceManager resourceManager, MyGameRepository gameRepository)
        {
            this.resourceManager = resourceManager;
            this.gameRepository = gameRepository;
        }

        public void SaveData()
        {
            var resources = resourceManager.GetResources();
            var resourceData = resources.ConvertAll(ConvertUnitToData);
            gameRepository.SetData(resourceData);
            Debug.Log($"Resources saved, amount of saved {resources.Count}");
        }

        private ResourceData ConvertUnitToData(ResourceObject resource)
        {
            return new ResourceData(
                resourceType: resource.resourceType,
                remainingCount: resource.remainingCount
            );
        }

        public void LoadData()
        {
        }

        private class ResourceData
        {
            public ResourceType ResourceType { get; }
            public int RemainingCount { get; }

            public ResourceData(
                ResourceType resourceType,
                int remainingCount)
            {
                ResourceType = resourceType;
                RemainingCount = remainingCount;
            }
        }
    }
}