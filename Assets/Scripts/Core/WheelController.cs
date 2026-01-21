using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

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
        [SerializeField] private TextMeshProUGUI[] sliceTexts;

        private WheelConfiguration currentConfig;
        private WheelSliceData[] currentSlices;
        private bool isSpinning = false;

        public bool IsSpinning => isSpinning;

        public void SetupWheel(WheelConfiguration config, WheelSliceData[] slices)
        {
            currentConfig = config;
            currentSlices = slices;

            if (wheelBaseImage != null && config.wheelBase != null)
                wheelBaseImage.sprite = config.wheelBase;

            if (indicatorImage != null && config.indicator != null)
                indicatorImage.sprite = config.indicator;

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

            int sliceCount = currentSlices.Length;
            int resultIndex = UnityEngine.Random.Range(0, sliceCount);

            float sliceAngle = 360f / sliceCount;

            // Reverse the index for clockwise rotation
            int adjustedIndex = (sliceCount - resultIndex) % sliceCount;
            float targetAngle = adjustedIndex * sliceAngle;

            float fullSpins = 360f * UnityEngine.Random.Range(3, 6);
            float finalAngle = -(fullSpins + targetAngle);

            float duration = currentConfig.GetRandomSpinDuration();

            wheelRoot.localRotation = Quaternion.identity;

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