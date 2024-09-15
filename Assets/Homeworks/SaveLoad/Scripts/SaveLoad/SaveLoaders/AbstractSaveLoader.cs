namespace Homeworks.SaveLoad
{
    public abstract class AbstractSaveLoader<TData> : ISaveLoader
    {
        private readonly MyGameRepository gameRepository;

        public AbstractSaveLoader(MyGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public void SaveData()
        {
            var dataToSave = GetDataToSave();
            gameRepository.SetData(dataToSave);
        }

        public void LoadData()
        {
            if (gameRepository.TryGetData(out TData savedData))
            {
                HandleDataLoad(savedData);
            }
            else
            {
                HandleDataLoadMissing();
            }
        }

        protected abstract TData GetDataToSave();
        protected abstract void HandleDataLoad(TData savedData);

        protected virtual void HandleDataLoadMissing()
        {
        }
    }
}