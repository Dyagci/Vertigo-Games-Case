using UnityEngine;
using DG.Tweening;

namespace WheelOfFortune.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected float fadeDuration = 0.3f;

        protected bool isVisible;

        public bool IsVisible => isVisible;

        protected virtual void Awake()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        protected virtual void OnValidate()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(1f, fadeDuration).OnComplete(() =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                isVisible = true;
                OnShowComplete();
            });
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                isVisible = false;
                gameObject.SetActive(false);
                OnHideComplete();
            });
        }

        public virtual void ShowImmediate()
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            isVisible = true;
            OnShowComplete();
        }

        public virtual void HideImmediate()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            isVisible = false;
            gameObject.SetActive(false);
            OnHideComplete();
        }

        protected virtual void OnShowComplete() { }
        protected virtual void OnHideComplete() { }
    }
}
