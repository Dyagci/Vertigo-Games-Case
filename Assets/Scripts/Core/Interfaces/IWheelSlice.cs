namespace WheelOfFortune.Core
{
    public interface IWheelSlice
    {
        IReward Reward { get; }
        bool IsBomb { get; }
    }
}
