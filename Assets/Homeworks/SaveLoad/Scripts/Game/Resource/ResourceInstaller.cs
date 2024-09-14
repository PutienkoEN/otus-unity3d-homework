using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class ResourceInstaller : MonoInstaller
    {
        [SerializeField] private Transform resourceContainer;
        [SerializeField] private List<ResourceObject> resourcePrefabs;

        public override void InstallBindings()
        {
            var resources = FindObjectsOfType<ResourceObject>();

            Container
                .Bind<ResourceManager>()
                .AsSingle()
                .WithArguments(resources)
                .NonLazy();

            Container
                .Bind<ResourceSpawner>()
                .AsSingle()
                .WithArguments(resourceContainer, resourcePrefabs);
        }
    }
}