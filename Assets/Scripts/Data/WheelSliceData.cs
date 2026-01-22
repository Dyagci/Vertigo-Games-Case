using UnityEngine;

namespace WheelOfFortune.Core
{
    [System.Serializable]
    public class WheelSliceData
    {
        public RewardData reward;
        public int rewardAmount;

        public bool IsBomb => reward != null && reward.Type == RewardType.Bomb;
        public Sprite Icon => reward != null ? reward.icon : null;
        public string MultiplierText => IsBomb ? "" : $"x{rewardAmount}";
    }
}