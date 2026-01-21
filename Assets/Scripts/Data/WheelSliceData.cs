using UnityEngine;

namespace WheelOfFortune.Core
{
    [System.Serializable]
    public class WheelSliceData
    {
        public RewardType rewardType;
        public Sprite icon;
        public int rewardAmount;
        
        public bool IsBomb => rewardType == RewardType.Bomb;
        public string MultiplierText => IsBomb ? "" : $"x{rewardAmount}";
    }
}