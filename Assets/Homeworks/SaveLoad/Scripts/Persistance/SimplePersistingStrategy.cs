using UnityEngine;
using Zenject;

namespace Homeworks.SaveLoad.Scripts.Persistance
{
    public class SimplePersistingStrategy : IPersistingStrategy
    {
        private readonly string gameContextProperty;

        [Inject]
        public SimplePersistingStrategy(string gameContextProperty)
        {
            this.gameContextProperty = gameContextProperty;
        }

        public void Save(string valueToSave)
        {
            PlayerPrefs.SetString(gameContextProperty, valueToSave);
        }

        public bool TryLoad(out string value)
        {
            if (!PlayerPrefs.HasKey(gameContextProperty))
            {
                Debug.LogError("Failed to load data! There is no saved data to load!");
                value = default;
                return false;
            }


            value = PlayerPrefs.GetString(gameContextProperty);
            return true;
        }
    }
}