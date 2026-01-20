using UnityEngine;

namespace WheelOfFortune.Core
{
    public interface IReward
    {
        RewardType Type { get; }
        Sprite Icon { get; }
        string Name { get; }
        int Amount { get; }
    }
}
