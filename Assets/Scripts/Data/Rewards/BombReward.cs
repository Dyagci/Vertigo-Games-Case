using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "BombReward", menuName = "Game/Rewards/Bomb")]
    public class BombReward : RewardData
    {
        public override RewardType Type => RewardType.Bomb;
    }
}
