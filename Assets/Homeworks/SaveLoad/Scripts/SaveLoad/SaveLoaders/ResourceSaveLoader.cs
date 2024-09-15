using System;
using System.Collections.Generic;
using Homeworks.SaveLoad.Data;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class ResourceSaveLoader : AbstractSaveLoader<ResourceSaveLoader.ResourceDataStorage>
    {
        private readonly ResourceManager resourceManager;

        [Inject]
        public ResourceSaveLoader(
            MyGameRepository gameRepository,
            ResourceManager resourceManager) : base(gameRepository)
        {
            this.resourceManager = resourceManager;
        }

        protected override ResourceDataStorage GetDataToSave()
        {
            var resources = resourceManager.GetResources();
            var resourceData = resources.ConvertAll(ConvertToSaveData);

            return new ResourceDataStorage { Resources = resourceData };
        }

        private static ResourceData ConvertToSaveData(ResourceObject resource)
        {
            return new ResourceData
            {
                ResourceTypeUid = resource.resourceTypeUid,
                ResourceType = resource.resourceType,
                RemainingCount = resource.remainingCount,
                TransformData = TransformData.FromTransform(resource.transform)
            };
        }

        protected override void HandleDataLoad(ResourceDataStorage savedData)
        {
            savedData.Resources
                .ConvertAll(ConvertFromSaveData)
                .ForEach(resourceManager.CreateResource);
        }

        private static ResourceCreateCommand ConvertFromSaveData(ResourceData resourceData)
        {
            var transformData = resourceData.TransformData;
            return new ResourceCreateCommand(
                resourceData.ResourceTypeUid,
                resourceData.ResourceType,
                resourceData.RemainingCount,
                transformData.GetPositionAsVector3(),
                transformData.GetRotationAsQuaternion(),
                transformData.GetScaleAsVector3()
            );
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