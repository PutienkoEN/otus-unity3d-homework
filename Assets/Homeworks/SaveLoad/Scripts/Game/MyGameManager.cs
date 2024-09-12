using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class MyGameManager : MonoBehaviour
    {
        private SaveLoadManager saveLoadManager;

        [Inject]
        public void Construct(SaveLoadManager saveLoadManager)
        {
            this.saveLoadManager = saveLoadManager;
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