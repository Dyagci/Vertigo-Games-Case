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

        [Header("UI Settings")]
        public string rewardInfoText;
        public Color textColor = Color.white;

        public WheelSliceData[] slices = new WheelSliceData[8];

        public float GetRandomSpinDuration()
        {
            return Random.Range(minSpinDuration, maxSpinDuration);
        }
    }
}