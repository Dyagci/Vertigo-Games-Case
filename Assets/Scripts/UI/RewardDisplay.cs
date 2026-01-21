using UnityEngine;
using System.Collections.Generic;
using WheelOfFortune.Core;
using WheelOfFortune.Managers;

namespace WheelOfFortune.UI
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField] private Transform rewardsContainer;
        [SerializeField] private RewardItem rewardItemPrefab;

        private Dictionary<Sprite, RewardItem> rewardItems = new Dictionary<Sprite, RewardItem>();

        private void Start()
        {
            GameManager.Instance.OnRewardCollected += AddRewardIcon;
            GameManager.Instance.OnGameOver += ClearRewards;
            GameManager.Instance.OnGameWin += ClearRewards;
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnRewardCollected -= AddRewardIcon;
                GameManager.Instance.OnGameOver -= ClearRewards;
                GameManager.Instance.OnGameWin -= ClearRewards;
            }
        }

        private void AddRewardIcon(WheelSliceData reward)
        {
            if (reward.icon == null) return;


            if (rewardItems.TryGetValue(reward.icon, out RewardItem existingItem))
            {
                existingItem.AddAmount(reward.rewardAmount);
            }
            else
            {
                RewardItem item = Instantiate(rewardItemPrefab, rewardsContainer);
                item.Setup(reward);
                rewardItems.Add(reward.icon, item);
            }
        }

        private void ClearRewards()
        {
            foreach (Transform child in rewardsContainer)
            {
                Destroy(child.gameObject);
            }
            rewardItems.Clear();
        }


    }
}