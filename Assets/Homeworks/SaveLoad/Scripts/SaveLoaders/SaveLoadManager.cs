using System.Collections.Generic;
using Sirenix.OdinInspector;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadManager
    {
        private readonly List<ISaveLoader> saveLoaders;
        private readonly MyGameRepository gameRepository;

        [Inject]
        public SaveLoadManager(List<ISaveLoader> saveLoaders, MyGameRepository gameRepository)
        {
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository;
        }

        [Button]
        public void SaveGame()
        {
            saveLoaders.ForEach(saveLoader => saveLoader.SaveGame());
            gameRepository.Save();
        }

        [Button]
        public void LoadGame()
        {
            saveLoaders.ForEach(saveLoader => saveLoader.LoadGame());
        }
    }
}