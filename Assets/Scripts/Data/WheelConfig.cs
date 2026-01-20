using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Core;

namespace WheelOfFortune.Data
{
    [CreateAssetMenu(fileName = "NewWheelConfig", menuName = "WheelOfFortune/Wheel Config")]
    public class WheelConfig : ScriptableObject
    {
        [Header("Wheel Settings")]
        [SerializeField] private ZoneType wheelType;
        [SerializeField] private Sprite wheelSprite;
        [SerializeField] private Sprite indicatorSprite;

        [Header("Slices")]
        [SerializeField] private List<WheelSliceData> slices = new List<WheelSliceData>();

        public ZoneType WheelType => wheelType;
        public Sprite WheelSprite => wheelSprite;
        public Sprite IndicatorSprite => indicatorSprite;
        public IReadOnlyList<WheelSliceData> Slices => slices;
        public int SliceCount => slices.Count;

        public WheelSliceData GetSlice(int index)
        {
            if (index < 0 || index >= slices.Count)
            {
                return null;
            }
            return slices[index];
        }

        public bool HasBomb()
        {
            foreach (var slice in slices)
            {
                if (slice.IsBomb)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
