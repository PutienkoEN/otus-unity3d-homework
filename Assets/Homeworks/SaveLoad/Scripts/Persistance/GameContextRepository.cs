using System.Collections.Generic;
using Newtonsoft.Json;
using Zenject;

namespace Homeworks.SaveLoad.Scripts.Persistance
{
    public class GameContextRepository
    {
        private readonly IPersistingStrategy persistingStrategy;

        private Dictionary<string, string> context = new();

        [Inject]
        public GameContextRepository(IPersistingStrategy persistingStrategy)
        {
            this.persistingStrategy = persistingStrategy;
        }

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            context[typeof(T).Name] = serializedData;
        }

        public bool TryGetData<T>(out T value)
        {
            if (context.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void Save()
        {
            var serializedContext = JsonConvert.SerializeObject(context);
            persistingStrategy.Save(serializedContext);
        }

        public void Load()
        {
            if (!persistingStrategy.TryLoad(out var loadedContext))
            {
                return;
            }

            context = JsonConvert.DeserializeObject<Dictionary<string, string>>(loadedContext);
        }
    }
}