using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    /*
     * To be used for testing in Editor and gather all functionality in one place.
     */
    public class MyGameManager : MonoBehaviour
    {
        private SaveLoadManager saveLoadManager;

        [SerializeReference] private UnitManager unitManager;
        [SerializeReference] private ResourceManager resourceManager;

        [Inject]
        public void Construct(
            SaveLoadManager saveLoadManager,
            UnitManager unitManager,
            ResourceManager resourceManager)
        {
            this.saveLoadManager = saveLoadManager;
            this.unitManager = unitManager;
            this.resourceManager = resourceManager;
        }

        [Button]
        public void SaveGame()
        {
            saveLoadManager.SaveGame();
        }

        [Button]
        public void LoadGame()
        {
            saveLoadManager.LoadGame();
        }

        [Button]
        public void RemoveUnitsFromScene()
        {
            unitManager.RemoveUnitsFromScene();
        }

        [Button]
        public void RemoveResourcesFromScene()
        {
            resourceManager.RemoveAllResources();
        }
    }
}