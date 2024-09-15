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

        public ResourceObject SpawnResource(CreateResourceCommand createResourceCommand)
        {
            if (!resourcePrefabs.TryGetValue(createResourceCommand.ResourceTypeUid, out ResourceObject resourcePrefab))
            {
                throw new KeyNotFoundException(
                    $"There is no resource prefab for uid {createResourceCommand.ResourceType}");
            }

            var resourceObject = CreateGameObject(createResourceCommand, resourcePrefab);
            SetupResourceData(resourceObject, createResourceCommand);

            return resourceObject;
        }

        private ResourceObject CreateGameObject(
            CreateResourceCommand createResourceCommand,
            ResourceObject resourcePrefab)
        {
            var resourceObject = Object.Instantiate(
                resourcePrefab,
                createResourceCommand.Position,
                createResourceCommand.Rotation,
                resourceContainer
            );

            resourceObject.transform.localScale = createResourceCommand.Scale;

            return resourceObject;
        }

        private void SetupResourceData(ResourceObject resource, CreateResourceCommand createResourceCommand)
        {
            resource.resourceTypeUid = createResourceCommand.ResourceTypeUid;
            resource.resourceType = createResourceCommand.ResourceType;
            resource.remainingCount = createResourceCommand.RemainingCount;
        }
    }
}