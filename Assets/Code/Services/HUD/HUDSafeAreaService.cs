using Code.Providers.GameObject;
using UnityEngine;
using Zenject;
using Screen = UnityEngine.Device.Screen;

namespace Code.Services.HUD
{
    public class HUDSafeAreaService : IInitializable, IHUDSafeAreaService
    {
        private readonly ILevelObjectsProvider _objectsProvider;

        public HUDSafeAreaService(ILevelObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
        }

        public void Initialize()
        {
            var safeArea = Screen.safeArea;
            var anchorMax = safeArea.position + safeArea.size;

            if (Screen.safeArea.size == new Vector2(Screen.width, Screen.height))
            {
                return;
            }

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _objectsProvider.SafeArea.anchorMax = anchorMax;
        }

        public void SetSafeAreaMinAnchor(Vector2 value)
        {
            _objectsProvider.SafeArea.anchorMin = value;
        }

//TODO refactor using hud rect
        public float SafeAreaTopPadding()
        {
            return Screen.height / 2f - SafeAreaTopY();
        }

        private float SafeAreaTopY()
        {
            return _objectsProvider.SafeArea.rect.height / 2 * _objectsProvider.SafeArea.anchorMax.y * _objectsProvider.MainCanvas.scaleFactor;
        }
    }
}