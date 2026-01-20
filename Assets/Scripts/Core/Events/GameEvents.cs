using System;

namespace WheelOfFortune.Core
{
    public static class GameEvents
    {
        public static event Action<GameState> OnGameStateChanged;
        public static event Action<int, ZoneType> OnZoneChanged;
        public static event Action<IReward> OnRewardCollected;
        public static event Action OnBombHit;
        public static event Action OnSpinStarted;
        public static event Action<IWheelSlice> OnSpinEnded;
        public static event Action OnGameReset;

        public static void RaiseGameStateChanged(GameState newState)
        {
            OnGameStateChanged?.Invoke(newState);
        }

        public static void RaiseZoneChanged(int zoneNumber, ZoneType zoneType)
        {
            OnZoneChanged?.Invoke(zoneNumber, zoneType);
        }

        public static void RaiseRewardCollected(IReward reward)
        {
            OnRewardCollected?.Invoke(reward);
        }

        public static void RaiseBombHit()
        {
            OnBombHit?.Invoke();
        }

        public static void RaiseSpinStarted()
        {
            OnSpinStarted?.Invoke();
        }

        public static void RaiseSpinEnded(IWheelSlice result)
        {
            OnSpinEnded?.Invoke(result);
        }

        public static void RaiseGameReset()
        {
            OnGameReset?.Invoke();
        }

        public static void ClearAllListeners()
        {
            OnGameStateChanged = null;
            OnZoneChanged = null;
            OnRewardCollected = null;
            OnBombHit = null;
            OnSpinStarted = null;
            OnSpinEnded = null;
            OnGameReset = null;
        }
    }
}
