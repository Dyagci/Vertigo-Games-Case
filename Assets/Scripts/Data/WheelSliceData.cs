using UnityEngine;

namespace WheelOfFortune.Core
{
    [System.Serializable]
    public class WheelSliceData
    {
        public RewardData reward; // Yukarıdaki SO sınıflarından herhangi biri
        public int rewardAmount;

        // Kolaylık sağlaması için yardımcı propertyler
        public bool IsBomb => reward != null && reward.Type == RewardType.Bomb;
        public Sprite Icon => reward != null ? reward.icon : null;
        public string MultiplierText => IsBomb ? "" : $"x{rewardAmount}";
    }
}