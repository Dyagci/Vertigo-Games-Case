using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Data
{
    [CreateAssetMenu(fileName = "ZoneConfig", menuName = "WheelOfFortune/Zone Config")]
    public class ZoneConfig : ScriptableObject
    {
        [Header("Zone Rules")]
        [SerializeField] private int safeZoneInterval = 5;
        [SerializeField] private int superZoneInterval = 30;

        [Header("Wheel Assignments")]
        [SerializeField] private List<ZoneWheelMapping> normalZoneWheels = new List<ZoneWheelMapping>();
        [SerializeField] private WheelConfig safeZoneWheel;
        [SerializeField] private WheelConfig superZoneWheel;

        public int SafeZoneInterval => safeZoneInterval;
        public int SuperZoneInterval => superZoneInterval;
        public WheelConfig SafeZoneWheel => safeZoneWheel;
        public WheelConfig SuperZoneWheel => superZoneWheel;

        public bool IsSafeZone(int zoneNumber)
        {
            if (zoneNumber <= 0) return false;
            if (IsSuperZone(zoneNumber)) return false;
            return zoneNumber % safeZoneInterval == 0;
        }

        public bool IsSuperZone(int zoneNumber)
        {
            if (zoneNumber <= 0) return false;
            return zoneNumber % superZoneInterval == 0;
        }

        public WheelConfig GetWheelForZone(int zoneNumber)
        {
            if (IsSuperZone(zoneNumber))
            {
                return superZoneWheel;
            }

            if (IsSafeZone(zoneNumber))
            {
                return safeZoneWheel;
            }

            // Find the appropriate normal wheel based on zone range
            foreach (var mapping in normalZoneWheels)
            {
                if (zoneNumber >= mapping.fromZone && zoneNumber <= mapping.toZone)
                {
                    return mapping.wheelConfig;
                }
            }

            // Fallback to last normal wheel if zone exceeds all mappings
            if (normalZoneWheels.Count > 0)
            {
                return normalZoneWheels[normalZoneWheels.Count - 1].wheelConfig;
            }

            return null;
        }
    }

    [System.Serializable]
    public class ZoneWheelMapping
    {
        public int fromZone = 1;
        public int toZone = 5;
        public WheelConfig wheelConfig;
    }
}
