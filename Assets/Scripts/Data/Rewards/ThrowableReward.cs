using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "ThrowableReward", menuName = "Game/Rewards/Throwable")]
    public class ThrowableReward : RewardData
    {
        public override RewardType Type => RewardType.Throwable;
    }
}
