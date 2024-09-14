using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class MyGameRepository
    {
        private const string GameContextProperty = "Lesson/GameState";

        private Dictionary<string, string> context = new();

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

        public void Load()
        {
            if (!PlayerPrefs.HasKey(GameContextProperty))
            {
                Debug.LogError("Failed to load data! There is no saved data to load!");
                return;
            }

            var serializedContext = PlayerPrefs.GetString(GameContextProperty);
            context = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedContext);
        }

        public void Save()
        {
            var serializedContext = JsonConvert.SerializeObject(context);
            PlayerPrefs.SetString(GameContextProperty, serializedContext);
        }
    }
}