using System.Collections.Generic;
using Sirenix.OdinInspector;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadManager
    {
        private readonly List<ISaveLoader> saveLoaders;

        [Inject]
        public SaveLoadManager(List<ISaveLoader> saveLoaders)
        {
            this.saveLoaders = saveLoaders;
        }

        [Button]
        public void SaveGame()
        {
            saveLoaders.ForEach(saveLoader => saveLoader.SaveGame());
        }

        [Button]
        public void LoadGame()
        {
            saveLoaders.ForEach(saveLoader => saveLoader.LoadGame());
        }
    }
}