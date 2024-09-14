using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class UnitInstaller : MonoInstaller
    {
        [SerializeField] private GameObject unitPrefab;

        public override void InstallBindings()
        {
            var units = FindObjectsOfType<UnitObject>();

            Container
                .Bind<UnitManager>()
                .AsSingle()
                .WithArguments(units)
                .NonLazy();


            Container
                .Bind<UnitSpawner>()
                .AsSingle()
                .WithArguments(unitPrefab);
        }
    }
}