using Homeworks.SaveLoad.Scripts.Security;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad.Scripts.Persistance
{
    public class PersistenceInstaller : MonoInstaller
    {
        [SerializeField] private string gameContextProperty = "Lesson/GameState";

        [SerializeField] private string encryptionKey =
            "72pg68Mczouyk658jsEux1SLXkli7MlyBuLsqh8I2S0r3819btORCEKj3wUgh6Q5";

        public override void InstallBindings()
        {
            Container
                .Bind<GameContextRepository>()
                .AsSingle();

            Container
                .Bind<AesEncryptor>()
                .AsSingle()
                .WithArguments(encryptionKey);

            Container
                .BindInterfacesAndSelfTo<AesPersistingStrategy>()
                .AsSingle()
                .WithArguments(new SimplePersistingStrategy(gameContextProperty));
        }
    }
}