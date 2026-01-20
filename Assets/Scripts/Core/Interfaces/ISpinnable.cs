using System;

namespace WheelOfFortune.Core
{
    public interface ISpinnable
    {
        void Spin();
        event Action<IWheelSlice> OnSpinComplete;
    }
}
