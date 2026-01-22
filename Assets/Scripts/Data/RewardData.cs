using UnityEngine;

namespace WheelOfFortune.Core
{
    public abstract class RewardData : ScriptableObject
    {
        [Header("Base Info")]
        public string rewardID;
        public Sprite icon;
        
        // Her alt sınıf bu tipi kendi içinde belirleyecek (hardcoded)
        public abstract RewardType Type { get; }
    }
}