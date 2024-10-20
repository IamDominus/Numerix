using Code.Providers;
using Code.Providers.GameObject;
using Code.Services.Ad;
using Code.ViewControllers;
using Code.ViewControllers.HUD;
using UnityEngine;

namespace Code.Gameplay.Providers
{
    public class DynamicBoundsProvider : IDynamicBoundsProvider
    {
        private const float BOT_EXTRA_PADDING = 50f;
        private const float WIDTH_OFFSET = 60f;
        private const float CELL_SIZE_OFFSET = 0.15f;

        public Vector2 FieldSize => _fieldSize;
        public Vector2 FieldPosition => _fieldPosition;
        public Vector2 CellSize => _cellSize;

        private readonly ILevelObjectsProvider _objectsProvider;
        private readonly IAdService _adService;
        private readonly ISelectedLevelProvider _selectedLevelProvider;
        private readonly IHUDViewController _hudViewController;

        private Vector2 _fieldSize;
        private Vector2 _fieldPosition;
        private Vector2 _cellSize;

        public DynamicBoundsProvider(ILevelObjectsProvider objectsProvider, IAdService adService, ISelectedLevelProvider selectedLevelProvider,
            IHUDViewController hudViewController)
        {
            _objectsProvider = objectsProvider;
            _adService = adService;
            _selectedLevelProvider = selectedLevelProvider;
            _hudViewController = hudViewController;
        }

        public void Initialize()
        {
            _fieldSize = CalculateFieldSize();
            _fieldPosition = CalculateFieldPosition();
            _cellSize = CalculateCellSize();

            UpdateHUDAnchor();
        }

        public Vector2 GetBlockInWorldPosition(int x, int y)
        {
            var totalXSize = CellSize.x * _selectedLevelProvider.Level.Value.y;
            var xOffset = (FieldSize.x - totalXSize) / (_selectedLevelProvider.Level.Value.y + 1);

            var totalYSize = CellSize.y * _selectedLevelProvider.Level.Value.x;
            var yOffset = (FieldSize.y - totalYSize) / (_selectedLevelProvider.Level.Value.x + 1);

            var fieldTopLeft = new Vector2(FieldSize.x / -2 + FieldPosition.x, FieldSize.y / 2 + FieldPosition.y);

            var xWorldOffset = y * (CellSize.x + xOffset);
            var xWorld = fieldTopLeft.x + CellSize.x / 2 + xOffset + xWorldOffset;

            var yWorldOffset = x * (CellSize.y + yOffset);
            var yWorld = fieldTopLeft.y - CellSize.y / 2 - yOffset - yWorldOffset;

            return new Vector2(xWorld, yWorld);
        }

        public Vector2 GetBlockInWorldPosition(Vector2Int position)
        {
            return GetBlockInWorldPosition(position.x, position.y);
        }

        private Vector2 CalculateFieldSize()
        {
            var scale = CalculateFieldScale();
            var aspectRatio = (float)_selectedLevelProvider.Level.Value.y / _selectedLevelProvider.Level.Value.x;

            var adjustedX = scale.y * aspectRatio;
            if (adjustedX > scale.x)
            {
                adjustedX = scale.x;
                scale.y = adjustedX / aspectRatio;
            }

            return new Vector2(adjustedX, scale.y);
        }

        private Vector2 CalculateFieldPosition()
        {
            var screenRatio = CalculateScreenRatio();
            var topOffset = _hudViewController.HUDBottomPoint();
            var botOffset = CalculateBannerHeight();
            var offsetDiff = botOffset - topOffset;

            var fieldCenter = offsetDiff / screenRatio / 2f;
            return new Vector2(0, fieldCenter);
        }

        private Vector2 CalculateFieldScale()
        {
            var screenRatio = CalculateScreenRatio();
            var topOffset = _hudViewController.HUDBottomPoint();
            var botOffset = CalculateBannerHeight();

            var fieldHeight = (Screen.height - topOffset - botOffset) / screenRatio;
            var fieldWidth = (Screen.width - WIDTH_OFFSET) / screenRatio;

            return new Vector2(fieldWidth, fieldHeight);
        }

        private float CalculateScreenRatio()
        {
            return Screen.height / (_objectsProvider.MainCamera.orthographicSize * 2);
        }

        private float CalculateBannerHeight()
        {
            var bannerHeight = _adService.GetBannerHeightInPixels();
            var screenDensity = Screen.dpi / 160f;

            return bannerHeight * screenDensity + BOT_EXTRA_PADDING;
        }

        private Vector2 CalculateCellSize()
        {
            var minDimension = Mathf.Min(_selectedLevelProvider.Level.Value.x, _selectedLevelProvider.Level.Value.y);
            var minFieldSize = Mathf.Min(FieldSize.x, FieldSize.y);
            var size = minFieldSize / minDimension - CELL_SIZE_OFFSET;

            return new Vector2(size, size);
        }

        private void UpdateHUDAnchor()
        {
            var fieldTop = _fieldPosition.y + _fieldSize.y / 2;
            var fieldTopInPixels = fieldTop * CalculateScreenRatio();
            var safeAreaMinAnchor = (Screen.height / 2f + fieldTopInPixels) / Screen.height;
            _hudViewController.UpdateMinAnchor(new Vector2(0, safeAreaMinAnchor));
        }
    }
}