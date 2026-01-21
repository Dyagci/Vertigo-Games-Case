using UnityEngine;
using UnityEngine.UI;
using System;

namespace WheelOfFortune.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonBase : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;

        [Header("Sprites")]
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite disabledSprite;

        public event Action OnClick;

        private void OnValidate()
        {
            if (button == null)
                button = GetComponent<Button>();
            if (buttonImage == null)
                buttonImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(HandleClick);
        }

        private void HandleClick()
        {
            OnClick?.Invoke();
        }

        public void SetInteractable(bool interactable)
        {
            button.interactable = interactable;

            // Only swap sprites if both are assigned
            if (buttonImage != null)
            {
                if (interactable && activeSprite != null)
                {
                    buttonImage.sprite = activeSprite;
                }
                else if (!interactable && disabledSprite != null)
                {
                    buttonImage.sprite = disabledSprite;
                }
            }
        }
    }
}