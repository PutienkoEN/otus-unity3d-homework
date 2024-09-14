using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class MyGameManager : MonoBehaviour
    {
        private SaveLoadManager saveLoadManager;

        [SerializeReference] private UnitManager unitManager;

        [Inject]
        public void Construct(SaveLoadManager saveLoadManager, UnitManager unitManager)
        {
            this.saveLoadManager = saveLoadManager;
            this.unitManager = unitManager;
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
    }
}