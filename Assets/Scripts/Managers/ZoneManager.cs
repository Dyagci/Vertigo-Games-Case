using UnityEngine;
using WheelOfFortune.Core;
using WheelOfFortune.Data;

namespace WheelOfFortune.Managers
{
    public class ZoneManager : Singleton<ZoneManager>
    {
        [SerializeField] private ZoneConfig zoneConfig;

        private int currentZone = 1;
        private ZoneType currentZoneType;

        public int CurrentZone => currentZone;
        public ZoneType CurrentZoneType => currentZoneType;
        public ZoneConfig ZoneConfig => zoneConfig;

        protected override void Awake()
        {
            base.Awake();
            SubscribeToEvents();
        }

        private void Start()
        {
            UpdateZoneType();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameEvents.OnGameReset += OnGameReset;
        }

        private void UnsubscribeFromEvents()
        {
            GameEvents.OnGameReset -= OnGameReset;
        }

        public void AdvanceToNextZone()
        {
            currentZone++;
            UpdateZoneType();
            GameEvents.RaiseZoneChanged(currentZone, currentZoneType);
        }

        private void UpdateZoneType()
        {
            if (zoneConfig == null)
            {
                currentZoneType = ZoneType.Normal;
                return;
            }

            if (zoneConfig.IsSuperZone(currentZone))
            {
                currentZoneType = ZoneType.Super;
            }
            else if (zoneConfig.IsSafeZone(currentZone))
            {
                currentZoneType = ZoneType.Safe;
            }
            else
            {
                currentZoneType = ZoneType.Normal;
            }
        }

        public WheelConfig GetCurrentWheelConfig()
        {
            if (zoneConfig == null)
            {
                Debug.LogError("ZoneConfig is not assigned to ZoneManager!");
                return null;
            }

            return zoneConfig.GetWheelForZone(currentZone);
        }

        public bool IsSafeZone()
        {
            return currentZoneType == ZoneType.Safe;
        }

        public bool IsSuperZone()
        {
            return currentZoneType == ZoneType.Super;
        }

        public bool CanPlayerLeave()
        {
            return currentZoneType == ZoneType.Safe || currentZoneType == ZoneType.Super;
        }

        public bool HasBombInCurrentZone()
        {
            var wheelConfig = GetCurrentWheelConfig();
            if (wheelConfig == null) return false;
            return wheelConfig.HasBomb();
        }

        private void OnGameReset()
        {
            currentZone = 1;
            UpdateZoneType();
            GameEvents.RaiseZoneChanged(currentZone, currentZoneType);
        }

        public void ResetZone()
        {
            currentZone = 1;
            UpdateZoneType();
        }
    }
}
