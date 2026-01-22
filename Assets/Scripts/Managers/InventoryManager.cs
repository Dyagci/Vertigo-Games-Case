using UnityEngine;
using System;
using System.Collections.Generic;
using WheelOfFortune.Core;

namespace WheelOfFortune.Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance { get; private set; }

        public event Action OnInventoryUpdated;

        // Visible in Editor
        [Header("Collected Rewards (Read Only)")]
        [SerializeField] private List<InventoryEntry> inventoryDisplay = new List<InventoryEntry>();

        // Key: RewardData SO, Value: total collected amount
        private Dictionary<RewardData, int> inventory = new Dictionary<RewardData, int>();

        public IReadOnlyDictionary<RewardData, int> Inventory => inventory;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void AddReward(RewardData reward, int amount)
        {
            if (reward == null || reward.Type == RewardType.Bomb) return;

            if (inventory.ContainsKey(reward))
                inventory[reward] += amount;
            else
                inventory[reward] = amount;

            UpdateEditorDisplay();
            OnInventoryUpdated?.Invoke();
        }

        public void AddRewardsFromSession(List<WheelSliceData> sessionRewards)
        {
            foreach (var sliceData in sessionRewards)
            {
                if (sliceData.reward != null && !sliceData.IsBomb)
                {
                    AddReward(sliceData.reward, sliceData.rewardAmount);
                }
            }
        }

        public int GetRewardAmount(RewardData reward)
        {
            return inventory.TryGetValue(reward, out int amount) ? amount : 0;
        }

        public void ClearInventory()
        {
            inventory.Clear();
            UpdateEditorDisplay();
            OnInventoryUpdated?.Invoke();
        }

        public int GetTotalItemCount()
        {
            int total = 0;
            foreach (var kvp in inventory)
            {
                total += kvp.Value;
            }
            return total;
        }

        private void UpdateEditorDisplay()
        {
            inventoryDisplay.Clear();

            foreach (var kvp in inventory)
            {
                inventoryDisplay.Add(new InventoryEntry
                {
                    reward = kvp.Key,
                    amount = kvp.Value
                });
            }
        }
    }

    [System.Serializable]
    public class InventoryEntry
    {
        public RewardData reward;
        public int amount;
    }
}
