using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Core;
using WheelOfFortune.Data;

namespace WheelOfFortune.Managers
{
    public class RewardManager : Singleton<RewardManager>
    {
        private List<CollectedReward> collectedRewards = new List<CollectedReward>();

        public IReadOnlyList<CollectedReward> CollectedRewards => collectedRewards;
        public int TotalRewardsCount => collectedRewards.Count;

        protected override void Awake()
        {
            base.Awake();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameEvents.OnBombHit += OnBombHit;
            GameEvents.OnGameReset += OnGameReset;
        }

        private void UnsubscribeFromEvents()
        {
            GameEvents.OnBombHit -= OnBombHit;
            GameEvents.OnGameReset -= OnGameReset;
        }

        public void AddReward(RewardData reward)
        {
            if (reward == null) return;

            // Check if we already have this reward type to stack
            var existingReward = FindExistingReward(reward);
            if (existingReward != null)
            {
                existingReward.AddAmount(reward.Amount);
            }
            else
            {
                collectedRewards.Add(new CollectedReward(reward));
            }

            GameEvents.RaiseRewardCollected(reward);
        }

        private CollectedReward FindExistingReward(RewardData reward)
        {
            foreach (var collected in collectedRewards)
            {
                if (collected.RewardData == reward)
                {
                    return collected;
                }
            }
            return null;
        }

        public void ClearAllRewards()
        {
            collectedRewards.Clear();
        }

        private void OnBombHit()
        {
            ClearAllRewards();
        }

        private void OnGameReset()
        {
            ClearAllRewards();
        }

        public List<CollectedReward> GetRewardsSummary()
        {
            return new List<CollectedReward>(collectedRewards);
        }
    }

    [System.Serializable]
    public class CollectedReward
    {
        private RewardData rewardData;
        private int totalAmount;

        public RewardData RewardData => rewardData;
        public int TotalAmount => totalAmount;
        public string Name => rewardData.Name;
        public Sprite Icon => rewardData.Icon;
        public RewardType Type => rewardData.Type;

        public CollectedReward(RewardData reward)
        {
            rewardData = reward;
            totalAmount = reward.Amount;
        }

        public void AddAmount(int amount)
        {
            totalAmount += amount;
        }
    }
}
