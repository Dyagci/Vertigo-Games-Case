using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "WeaponReward", menuName = "Game/Rewards/Weapon")]
    public class WeaponReward : RewardData
    {
        public override RewardType Type => RewardType.Weapon;
    }
}
