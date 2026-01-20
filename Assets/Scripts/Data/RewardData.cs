using UnityEngine;
using WheelOfFortune.Core;

namespace WheelOfFortune.Data
{
    [CreateAssetMenu(fileName = "NewReward", menuName = "WheelOfFortune/Reward Data")]
    public class RewardData : ScriptableObject, IReward
    {
        [SerializeField] private string rewardName;
        [SerializeField] private RewardType rewardType;
        [SerializeField] private Sprite icon;
        [SerializeField] private int amount = 1;

        public string Name => rewardName;
        public RewardType Type => rewardType;
        public Sprite Icon => icon;
        public int Amount => amount;
    }
}
