using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "ChestReward", menuName = "Game/Rewards/Chest")]
    public class ChestReward : RewardData
    {
        public override RewardType Type => RewardType.Chest;
    }
}
