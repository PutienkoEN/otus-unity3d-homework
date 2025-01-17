using System;
using System.Collections.Generic;
using Lessons.Gameplay.CharacterInteraction;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Gameplay.Lesson_CharacterInteraction
{
    public sealed class HarvestResourceEngine : MonoBehaviour
    {
        public event Action<HarvestResourceOperation> OnStarted;

        public event Action<HarvestResourceOperation> OnStopped;

        public bool IsHarvesting
        {
            get { return this.operation != null; }
        }

        public HarvestResourceOperation CurrentOperation
        {
            get { return this.operation; }
        }

        private HarvestResourceOperation operation;

        [SerializeReference]
        private List<IHarvestResourceCondition> preconditions = new();

        [SerializeReference]
        private List<IHarvestResourceAction> preactions = new();

        public bool CanStartHarvest(HarvestResourceOperation operation)
        {
            if (this.IsHarvesting)
            {
                return false;
            }

            for (int i = 0, count = this.preconditions.Count; i < count; i++)
            {
                var conditions = this.preconditions[i];
                if (!conditions.IsTrue(operation))
                {
                    return false;
                }
            }
            
            return true;
        }

        public void StartHarvest(HarvestResourceOperation operation)
        {
            if (!this.CanStartHarvest(operation))
            {
                return;
            }

            this.DoPreactions(operation);
            Debug.Log("Engine: start");
            this.operation = operation;
            this.OnStarted?.Invoke(operation);
        }

        public void StopHarvest()
        {
            if (this.IsHarvesting)
            {
                Debug.Log("Engine: stop");
                var operation = this.operation;
                this.operation = null;
                this.OnStopped?.Invoke(operation);
            }
        }

        [Button]
        private void Complete()
        {
            if (this.IsHarvesting)
            {
                this.operation.isCompleted = true;
                this.StopHarvest();
            }
        }
        
        private void DoPreactions(HarvestResourceOperation operation)
        {
            for (int i = 0, count = this.preactions.Count; i < count; i++)
            {
                var action = this.preactions[i];
                action.Do(operation);
            }
        }
    }
}