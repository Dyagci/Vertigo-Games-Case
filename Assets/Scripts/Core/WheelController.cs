// WheelController.cs
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using WheelOfFortune.Managers;

namespace WheelOfFortune.Core
{
    public class WheelController : MonoBehaviour
    {
        public event Action OnSpinStarted;
        public event Action<WheelSliceData> OnSpinEnded;

        [Header("References")]
        [SerializeField] private RectTransform wheelRoot;
        [SerializeField] private Image wheelBaseImage;
        [SerializeField] private Image indicatorImage;
        [SerializeField] private Transform slicesContainer;

        [Header("Slice UI References")]
        [SerializeField] private Image[] sliceIcons;
        [SerializeField] private TMPro.TextMeshProUGUI[] sliceTexts;

        private WheelConfiguration currentConfig;
        private WheelSliceData[] currentSlices;
        private bool isSpinning = false;

        public bool IsSpinning => isSpinning;

        public void SetupWheel(WheelConfiguration config, WheelSliceData[] slices)
        {
            currentConfig = config;
            currentSlices = slices;

            // Update wheel visuals
            wheelBaseImage.sprite = config.wheelBase;
            indicatorImage.sprite = config.indicator;

            // Update slice visuals
            for (int i = 0; i < slices.Length && i < sliceIcons.Length; i++)
            {
                sliceIcons[i].sprite = slices[i].icon;
                sliceTexts[i].text = slices[i].MultiplierText;
            }
        }

        public void Spin()
        {
            if (isSpinning || currentConfig == null) return;

            isSpinning = true;
            OnSpinStarted?.Invoke();

            // Pick random result
            int sliceCount = currentSlices.Length;
            int resultIndex = UnityEngine.Random.Range(0, sliceCount);

            // Calculate target angle
            float sliceAngle = 360f / sliceCount;
            float targetAngle = -(resultIndex * sliceAngle);

            // Add extra full spins
            float fullSpins = 360f * UnityEngine.Random.Range(3, 6);
            float finalAngle = targetAngle - fullSpins;

            // Get random duration
            float duration = currentConfig.GetRandomSpinDuration();

            // Reset rotation before spinning
            wheelRoot.localRotation = Quaternion.identity;

            // Animate
            wheelRoot.DORotate(new Vector3(0, 0, finalAngle), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuart)
                .OnComplete(() =>
                {
                    isSpinning = false;
                    OnSpinEnded?.Invoke(currentSlices[resultIndex]);
                });
        }
    }
}