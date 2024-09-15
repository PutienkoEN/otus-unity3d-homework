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
            var resourceData = resources.ConvertAll(ConvertUnitToData);

            return new ResourceDataStorage
            {
                Resources = resourceData
            };
        }

        private static ResourceData ConvertUnitToData(ResourceObject resource)
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
                .ForEach(LoadResource);
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