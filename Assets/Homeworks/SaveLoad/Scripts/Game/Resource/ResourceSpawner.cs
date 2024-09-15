using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class ResourceSpawner
    {
        private readonly Transform resourceContainer;
        private readonly Dictionary<string, ResourceObject> resourcePrefabs;

        [Inject]
        public ResourceSpawner(Transform resourceContainer, List<ResourceObject> resourcePrefabs)
        {
            this.resourceContainer = resourceContainer;
            this.resourcePrefabs = resourcePrefabs
                .ToDictionary(resource => resource.resourceTypeUid, resource => resource);
        }

        public ResourceObject SpawnResource(ResourceCreateCommand resourceCreateCommand)
        {
            if (!resourcePrefabs.TryGetValue(resourceCreateCommand.ResourceTypeUid, out ResourceObject resourcePrefab))
            {
                throw new KeyNotFoundException(
                    $"There is no resource prefab for uid {resourceCreateCommand.ResourceType}");
            }

            var resourceObject = CreateGameObject(resourceCreateCommand, resourcePrefab);
            SetupResourceData(resourceObject, resourceCreateCommand);

            return resourceObject;
        }

        private ResourceObject CreateGameObject(
            ResourceCreateCommand resourceCreateCommand,
            ResourceObject resourcePrefab)
        {
            var resourceObject = Object.Instantiate(
                resourcePrefab,
                resourceCreateCommand.Position,
                resourceCreateCommand.Rotation,
                resourceContainer
            );

            resourceObject.transform.localScale = resourceCreateCommand.Scale;

            return resourceObject;
        }

        private void SetupResourceData(ResourceObject resource, ResourceCreateCommand resourceCreateCommand)
        {
            resource.resourceTypeUid = resourceCreateCommand.ResourceTypeUid;
            resource.resourceType = resourceCreateCommand.ResourceType;
            resource.remainingCount = resourceCreateCommand.RemainingCount;
        }
    }
}