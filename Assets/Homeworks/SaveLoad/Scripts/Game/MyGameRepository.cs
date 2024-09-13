using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public class MyGameRepository
    {
        private const string GameContextProperty = "Lesson/GameState";

        private readonly Dictionary<string, string> context = new();

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            context.Add(typeof(T).Name, serializedData);
        }

        public void Save()
        {
            var serializedContext = JsonConvert.SerializeObject(context);
            PlayerPrefs.SetString(GameContextProperty, serializedContext);
        }
    }
}