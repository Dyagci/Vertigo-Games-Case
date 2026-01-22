using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "CosmeticReward", menuName = "Game/Rewards/Cosmetic")]
    public class CosmeticReward : RewardData
    {
        public override RewardType Type => RewardType.Cosmetic;
    }
}
