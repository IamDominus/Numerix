using UnityEngine;

namespace Code.Services.SafeArea
{
    public class SafeAreaInitializer : MonoBehaviour
    {
        public void Awake()
        {
            var safeArea = Screen.safeArea;
            var anchorMax = safeArea.position + safeArea.size;

            if (Screen.safeArea.size == new Vector2(Screen.width, Screen.height))
            {
                return;
            }

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            var rectTransform = (RectTransform)gameObject.transform;
            rectTransform.anchorMax = anchorMax;
        }
    }
}