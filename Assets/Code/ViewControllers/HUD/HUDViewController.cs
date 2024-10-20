using Code.Infrastructure.Factories;
using Code.Providers.GameObject;
using Code.Views.HUD;
using UnityEngine;

namespace Code.ViewControllers.HUD
{
    public class HUDViewController : IHUDViewController
    {
        private readonly IUIFactory _uiFactory;
        private readonly ILevelObjectsProvider _objectsProvider;
        private HUDView _hudView;

        public HUDViewController(IUIFactory uiFactory, ILevelObjectsProvider objectsProvider)
        {
            _uiFactory = uiFactory;
            _objectsProvider = objectsProvider;
        }

        public void Show()
        {
            _hudView ??= _uiFactory.CreateHUD(_objectsProvider.HUDParent);
        }

        public float HUDBottomPoint()
        {
            var hudTopOffset = _objectsProvider.HUDParent.rect.height / 2 * _objectsProvider.HUDParent.anchorMax.y * _objectsProvider.MainCanvas.scaleFactor;
            var hudTopPosition = Screen.height / 2f - hudTopOffset;
            var hudRect = ((RectTransform)_hudView.transform).rect;
            return hudRect.height * _objectsProvider.MainCanvas.scaleFactor + hudTopPosition;
        }

        public void UpdateMinAnchor(Vector2 minAnchor)
        {
            _objectsProvider.HUDParent.anchorMin = minAnchor;
        }
    }
}