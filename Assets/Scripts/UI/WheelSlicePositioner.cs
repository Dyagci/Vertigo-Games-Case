using UnityEngine;

namespace WheelOfFortune.UI
{
    [ExecuteInEditMode]
    public class WheelSlicePositioner : MonoBehaviour
    {
        [SerializeField] private float radius = 150f;
        [SerializeField] private float textOffset = 30f;

        private void OnValidate()
        {
            PositionSlices();
        }

        private void PositionSlices()
        {
            int sliceCount = transform.childCount;

            for (int i = 0; i < sliceCount; i++)
            {
                RectTransform slice = transform.GetChild(i) as RectTransform;
                if (slice == null) continue;

                float angle = i * (360f / sliceCount);
                float rad = angle * Mathf.Deg2Rad;

                float x = Mathf.Sin(rad) * radius;
                float y = Mathf.Cos(rad) * radius;

                slice.anchoredPosition = new Vector2(x, y);
                slice.localRotation = Quaternion.Euler(0, 0, -angle);

                // Text is second child (index 1), push toward center
                if (slice.childCount > 1)
                {
                    RectTransform textRect = slice.GetChild(1) as RectTransform;
                    if (textRect != null)
                    {
                        textRect.anchoredPosition = new Vector2(0, -textOffset);
                    }
                }
            }
        }
    }
}
