using System;
using System.Collections.Generic;
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
            var resourceDataStorage = new ResourceDataStorage(resourceData);

            gameRepository.SetData(resourceDataStorage);
            Debug.Log($"Resources saved, amount of saved {resources.Count}");
        }

        private ResourceData ConvertUnitToData(ResourceObject resource)
        {
            return new ResourceData(
                resourceTypeUid: resource.resourceTypeUid,
                resourceType: resource.resourceType,
                remainingCount: resource.remainingCount
            );
        }

        public void LoadData()
        {
            if (gameRepository.TryGetData<ResourceDataStorage>(out var resourceDataStorage))
            {
                resourceDataStorage.Resources.ForEach(LoadResource);
                Debug.Log($"Resources loaded, amount of loaded {resourceDataStorage.Resources.Count}");
            }
        }

        private void LoadResource(ResourceData resourceData)
        {
            var createResourceCommand = new CreateResourceCommand(
                resourceData.ResourceTypeUid,
                resourceData.ResourceType,
                resourceData.RemainingCount
            );

            resourceManager.CreateResource(createResourceCommand);
        }

        [Serializable]
        private class ResourceDataStorage
        {
            public List<ResourceData> Resources { get; }

            public ResourceDataStorage(List<ResourceData> resources)
            {
                Resources = resources;
            }
        }

        [Serializable]
        private class ResourceData
        {
            public string ResourceTypeUid { get; }
            public ResourceType ResourceType { get; }
            public int RemainingCount { get; }

            public ResourceData(
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
}