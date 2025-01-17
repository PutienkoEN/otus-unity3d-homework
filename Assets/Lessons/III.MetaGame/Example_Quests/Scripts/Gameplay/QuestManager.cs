using System;
using Game.Gameplay.Player;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Meta
{
    public sealed class QuestManager : MonoBehaviour,
        IGameConstructElement,
        IGameStartElement,
        IGameFinishElement
    {
        public event Action<Quest> OnQuestTaken;

        public event Action<Quest> OnQuestStarted;

        public event Action<Quest> OnQuestCompleted;

        public event Action<Quest> OnQuestRewardReceived;

        [ReadOnly]
        [ShowInInspector]
        public bool QuestExists
        {
            get { return this.Quest != null; }
        }

        [ReadOnly]
        [ShowInInspector]
        public Quest Quest { get; private set; }

        [SerializeField]
        private QuestSelector selector;

        [SerializeField]
        private QuestFactory factory;

        private MoneyStorage moneyStorage;

        private MoneyPanel moneyPanel;

        [Button]
        public bool CanTakeNewQuest()
        {
            return !this.QuestExists;
        }

        [Button]
        public void TakeNewQuest()
        {
            if (!this.CanTakeNewQuest())
            {
                throw new Exception("Can not take current quest!");
            }

            QuestConfig questConfig = this.selector.SelectRandomQuest();
            Quest quest = this.factory.CreateQuest(questConfig);

            this.Quest = quest;
            this.Quest.OnCompleted += this.OnCompleteQuest;

            this.Quest.Start();
            this.OnQuestStarted?.Invoke(quest);
            this.OnQuestTaken?.Invoke(quest);
        }

        [Button]
        public bool CanReceiveReward()
        {
            if (!this.QuestExists)
            {
                return false;
            }

            return this.Quest.State == QuestState.COMPLETED;
        }

        [Button]
        public void ReceiveReward()
        {
            if (!this.CanReceiveReward())
            {
                throw new Exception("Can't receive quest reward!");
            }

            var moneyReward = this.Quest.MoneyReward;
            this.moneyStorage.EarnMoney(moneyReward);
            this.moneyPanel.IncrementMoney(moneyReward);

            var quest = this.Quest;
            quest.OnCompleted -= this.OnCompleteQuest;
            this.Quest = null;

            this.factory.DestroyQuest(quest);
            this.OnQuestRewardReceived?.Invoke(quest);
        }

        public void SetupQuest(QuestConfig config)
        {
            this.Quest = this.factory.CreateQuest(config);
        }

        private void OnCompleteQuest(Quest quest)
        {
            this.OnQuestCompleted?.Invoke(quest);
        }

        void IGameConstructElement.ConstructGame(GameContext context)
        {
            this.moneyStorage = context.GetService<MoneyStorage>();
            this.moneyPanel = context.GetService<MoneyPanel>();
        }

        void IGameStartElement.StartGame()
        {
            if (this.QuestExists)
            {
                this.Quest.OnCompleted += this.OnCompleteQuest;
                this.Quest.Start();
                this.OnQuestStarted?.Invoke(this.Quest);
            }
        }

        void IGameFinishElement.FinishGame()
        {
            if (this.QuestExists)
            {
                this.Quest.OnCompleted -= this.OnCompleteQuest;
                this.Quest.Stop();
            }
        }
    }
}