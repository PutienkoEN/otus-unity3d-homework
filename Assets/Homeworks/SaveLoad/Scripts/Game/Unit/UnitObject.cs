using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class UnitObject : MonoBehaviour
    {
        [SerializeField] public string unitTypeUid;
        [SerializeField] public int hitPoints;
        [SerializeField] public int speed;
        [SerializeField] public int damage;

        public event Action<UnitObject> OnViewDestroy;

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke(this);
        }
    }
}