using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class ResourceManager
    {
        private readonly ResourceSpawner resourceSpawner;
        private readonly List<ResourceObject> resources;

        [Inject]
        public ResourceManager(ResourceSpawner resourceSpawner, ResourceObject[] resources)
        {
            this.resourceSpawner = resourceSpawner;

            this.resources = resources.ToList();
            this.resources
                .ForEach(resource => resource.OnViewDestroy += OnViewDestroy);
        }

        private void OnViewDestroy(ResourceObject resource)
        {
            resources.Remove(resource);
            resource.OnViewDestroy -= OnViewDestroy;
        }

        public List<ResourceObject> GetResources()
        {
            return resources;
        }

        public void CreateResource(ResourceCreateCommand resourceCreate)
        {
            var resource = resourceSpawner.SpawnResource(resourceCreate);
            resource.OnViewDestroy += OnViewDestroy;
            resources.Add(resource);
        }

        public void RemoveAllResources()
        {
            resources
                .ConvertAll(resource => resource.gameObject)
                .ForEach(Object.Destroy);
        }
    }
}