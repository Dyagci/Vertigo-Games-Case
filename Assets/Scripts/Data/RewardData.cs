using UnityEngine;

namespace WheelOfFortune.Core
{
    public abstract class RewardData : ScriptableObject
    {
        [Header("Base Info")]
        public string rewardID;
        public Sprite icon;
        
        public abstract RewardType Type { get; }
    }
}