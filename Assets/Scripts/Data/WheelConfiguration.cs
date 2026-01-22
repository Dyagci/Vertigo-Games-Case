using UnityEngine;

namespace WheelOfFortune.Core
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Game/Wheel Configuration")]
    public class WheelConfiguration : ScriptableObject
    {
        public WheelType wheelType;
        public Sprite wheelBase;
        public Sprite indicator;

        [Header("Spin Settings")]
        public float minSpinDuration = 2f;
        public float maxSpinDuration = 4f;

        [Header("Zone Scaling")]
        [Tooltip("Additional multiplier added per zone (e.g., 0.1 = +10% per zone)")]
        public float zoneScalingFactor = 0.1f;

        [Header("UI Settings")]
        public string rewardInfoText;
        public Color textColor = Color.white;

        public WheelSliceData[] slices = new WheelSliceData[8];

        public float GetRandomSpinDuration()
        {
            return Random.Range(minSpinDuration, maxSpinDuration);
        }

        public WheelSliceData[] GetScaledSlices(int currentZone)
        {
            float zoneMultiplier = 1f + (zoneScalingFactor * (currentZone - 1));
            WheelSliceData[] scaledSlices = new WheelSliceData[slices.Length];

            for (int i = 0; i < slices.Length; i++)
            {
                scaledSlices[i] = new WheelSliceData
                {
                    reward = slices[i].reward,
                    rewardAmount = Mathf.RoundToInt(slices[i].rewardAmount * zoneMultiplier)
                };
            }

            return scaledSlices;
        }
    }
}