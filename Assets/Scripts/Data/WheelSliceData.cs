using UnityEngine;

namespace WheelOfFortune.Data
{
    [System.Serializable]
    public class WheelSliceData
    {
        [SerializeField] private RewardData reward;
        [SerializeField] private bool isBomb;

        public RewardData Reward => reward;
        public bool IsBomb => isBomb;
    }
}
