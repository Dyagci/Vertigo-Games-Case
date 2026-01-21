using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WheelOfFortune.Core;

namespace WheelOfFortune.UI
{
    public class RewardItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI valueText;

        private Sprite currentIcon;
        private int totalAmount;

        private void OnValidate()
        {
            if (iconImage == null)
                iconImage = GetComponentInChildren<Image>();
            if (valueText == null)
                valueText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Setup(WheelSliceData data)
        {
            currentIcon = data.icon;
            totalAmount = data.rewardAmount;

            if (iconImage != null && data.icon != null)
                iconImage.sprite = data.icon;

            UpdateText();
        }

        public void AddAmount(int amount)
        {
            totalAmount += amount;
            UpdateText();
        }

        private void UpdateText()
        {
            if (valueText != null)
                valueText.text = $"x{totalAmount}";
        }

        public Sprite GetIcon()
        {
            return currentIcon;
        }
    }
}