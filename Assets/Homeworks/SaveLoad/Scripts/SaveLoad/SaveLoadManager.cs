using System.Collections.Generic;
using Homeworks.SaveLoad.Scripts.Persistance;
using Zenject;

namespace Homeworks.SaveLoad
{
    public class SaveLoadManager
    {
        private readonly List<ISaveLoader> saveLoaders;
        private readonly GameContextRepository gameRepository;

        [Inject]
        public SaveLoadManager(List<ISaveLoader> saveLoaders, GameContextRepository gameRepository)
        {
            this.saveLoaders = saveLoaders;
            this.gameRepository = gameRepository;
        }

        public void SaveGame()
        {
            saveLoaders.ForEach(saveLoader => saveLoader.SaveData());
            gameRepository.Save();
        }

        public void LoadGame()
        {
            gameRepository.Load();
            saveLoaders.ForEach(saveLoader => saveLoader.LoadData());
        }
    }
}