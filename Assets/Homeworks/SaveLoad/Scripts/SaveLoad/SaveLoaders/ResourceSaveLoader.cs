using System;
using System.Collections.Generic;
using Homeworks.SaveLoad.Data;
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

            var resourceDataStorage = new ResourceDataStorage
            {
                Resources = resourceData
            };

            gameRepository.SetData(resourceDataStorage);
            Debug.Log($"Resources saved, amount of saved {resources.Count}");
        }

        private ResourceData ConvertUnitToData(ResourceObject resource)
        {
            return new ResourceData
            {
                ResourceTypeUid = resource.resourceTypeUid,
                ResourceType = resource.resourceType,
                RemainingCount = resource.remainingCount,
                TransformData = TransformData.FromTransform(resource.transform)
            };
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
            var transformData = resourceData.TransformData;
            var createResourceCommand = new CreateResourceCommand(
                resourceData.ResourceTypeUid,
                resourceData.ResourceType,
                resourceData.RemainingCount,
                transformData.GetPositionAsVector3(),
                transformData.GetRotationAsQuaternion(),
                transformData.GetScaleAsVector3()
            );

            resourceManager.CreateResource(createResourceCommand);
        }

        [Serializable]
        public class ResourceDataStorage
        {
            public List<ResourceData> Resources { get; set; }
        }

        [Serializable]
        public class ResourceData
        {
            public string ResourceTypeUid { get; set; }
            public ResourceType ResourceType { get; set; }
            public int RemainingCount { get; set; }
            public TransformData TransformData { get; set; }
        }
    }
}