using Code.Providers.GameObject;
using Code.Views;
using UnityEngine;

namespace Code.Providers
{
    public class DynamicBoundsProvider : IDynamicBoundsProvider
    {
        private const float BOT_PADDING = 50f;

        public Vector2 FieldSize => _fieldSize;
        public Vector2 FieldPosition => _fieldPosition;
        public Vector2 CellSize => _cellSize;


        private readonly ILevelObjectsProvider _objectsProvider;
        private readonly IAddService _addService;

        private Vector2 _fieldSize;
        private Vector2 _fieldPosition;
        private Vector2 _cellSize;
        private Rect _hudRect;

        public DynamicBoundsProvider(ILevelObjectsProvider objectsProvider, IAddService addService)
        {
            _objectsProvider = objectsProvider;
            _addService = addService;
        }

        public void Initialize()
        {
            _hudRect = ((RectTransform)Resources.Load<HUDView>("HUD").transform).rect;
            _fieldSize = CalculateFieldSize();
            _fieldPosition = CalculateFieldPosition();
            _cellSize = CalculateCellSize();
        }

        public Vector2 GetBlockInWorldPosition(int x, int y)
        {
            var totalXSize = CellSize.x * Constants.DIMENSIONS.y;
            var xOffset = (FieldSize.x - totalXSize) / (Constants.DIMENSIONS.y + 1);

            var totalYSize = CellSize.y * Constants.DIMENSIONS.x;
            var yOffset = (FieldSize.y - totalYSize) / (Constants.DIMENSIONS.x + 1);

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
            var aspectRatio = (float)Constants.DIMENSIONS.y / Constants.DIMENSIONS.x;

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
            var ratio = CalculateScreenRatio();
            var topPadding = _hudRect.height * _objectsProvider.MainCanvas.scaleFactor;
            var botPadding = CalculateBannerHeight();
            var paddingDifference = botPadding - topPadding;

            var fieldCenter = paddingDifference / ratio / 2f;
            return new Vector2(0, fieldCenter);
        }

        private Vector2 CalculateFieldScale()
        {
            var ratio = CalculateScreenRatio();
            var topPadding = _hudRect.height * _objectsProvider.MainCanvas.scaleFactor;
            var botPadding = CalculateBannerHeight();

            var fieldHeight = (Screen.height - topPadding - botPadding) / ratio;
            const float widthPadding = 60f;
            var fieldWidth = (Screen.width - widthPadding) / ratio;

            return new Vector2(fieldWidth, fieldHeight);
        }

        private float CalculateScreenRatio()
        {
            return Screen.height / (_objectsProvider.MainCamera.orthographicSize * 2);
        }

        private float CalculateBannerHeight()
        {
            var bannerHeight = _addService.GetBannerHeightInPixels();
            var screenDensity = Screen.dpi / 160f;

            return bannerHeight * screenDensity + BOT_PADDING;
        }

        private Vector2 CalculateCellSize()
        {
            var minDimension = Mathf.Min(Constants.DIMENSIONS.x, Constants.DIMENSIONS.y);
            var minFieldSize = Mathf.Min(FieldSize.x, FieldSize.y);
            var size = minFieldSize / minDimension;
            size -= 0.15f;
            return new Vector2(size, size);
        }
    }
}