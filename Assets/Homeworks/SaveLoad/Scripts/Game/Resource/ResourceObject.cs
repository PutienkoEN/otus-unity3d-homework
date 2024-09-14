using System;
using UnityEngine;

namespace Homeworks.SaveLoad
{
    public sealed class ResourceObject : MonoBehaviour
    {
        [SerializeField] public ResourceType resourceType;
        [SerializeField] public int remainingCount;

        public event Action<ResourceObject> OnViewDestroy;

        private void OnDestroy()
        {
            OnViewDestroy?.Invoke(this);
        }
    }
}