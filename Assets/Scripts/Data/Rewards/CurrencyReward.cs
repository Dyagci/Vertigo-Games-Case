using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "CurrencyReward", menuName = "Game/Rewards/Currency")]
    public class CurrencyReward : RewardData
    {
        public override RewardType Type => RewardType.Currency;
    }
}
