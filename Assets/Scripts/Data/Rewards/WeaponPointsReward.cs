using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "WeaponPointsReward", menuName = "Game/Rewards/WeaponPoints")]
    public class WeaponPointsReward : RewardData
    {
        public override RewardType Type => RewardType.WeaponPoints;
    }
}
