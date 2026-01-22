using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "ConsumableReward", menuName = "Game/Rewards/Consumable")]
    public class ConsumableReward : RewardData
    {
        public override RewardType Type => RewardType.Consumable;
    }
}
