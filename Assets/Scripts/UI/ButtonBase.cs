// ButtonBase.cs
using UnityEngine;
using UnityEngine.UI;
using System;

namespace WheelOfFortune.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonBase : MonoBehaviour
    {
        [SerializeField] private Button button;

        public event Action OnClick;

        private void OnValidate()
        {
            if (button == null)
                button = GetComponent<Button>();
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
    }
}