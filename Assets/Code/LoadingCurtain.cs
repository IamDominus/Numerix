using DG.Tweening;
using UnityEngine;

namespace Code
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        private const float FADE_IN_DURATION = 0.5f;
        [SerializeField] private CanvasGroup _canvas;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()
        {
            _canvas.alpha = 1f;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _canvas.DOFade(0, FADE_IN_DURATION)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}