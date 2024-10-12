using Code.Providers;
using UnityEngine;
using Zenject;

namespace Code
{
    //TODO Remove
    public class FieldSizeHelper : MonoBehaviour
    {
        [SerializeField] private Vector2Int _dimensions = new Vector2Int(4, 4);
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _top;
        [SerializeField] private Transform _field;

        [SerializeField] private Canvas _canvas;

        // private LevelStaticData _levelData;
        private IAddService _addService;
        private IStaticDataProvider _staticDataProvider;
        private DynamicBoundsProvider _dynamicBoundsProvider;

        [Inject]
        private void Construct(IStaticDataProvider staticDataProvider, IAddService addService, DynamicBoundsProvider dynamicBoundsProvider)
        {
            _addService = addService;
            _staticDataProvider = staticDataProvider;
            _dynamicBoundsProvider = dynamicBoundsProvider;
        }

        public void Foo()
        {
            // _levelData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);


            // var ratio = Screen.height / (_camera.orthographicSize * 2);
            //
            // var topPadding = _top.rect.height * _canvas.scaleFactor;
            // var botPadding = CalculateBannerHeight();
            //
            // var fieldHeight = (Screen.height - topPadding - botPadding) / ratio;
            // var widthPadding = 60f;
            // var fieldWidth = (Screen.width - widthPadding) / ratio;
            // var fieldCenter = (botPadding - topPadding) / ratio / 2f;
            //
            // _field.position = new Vector3(0, fieldCenter, 1);
            //
            // var scale = new Vector2(fieldWidth, fieldHeight);
            //
            // var aspectRatio = (float)_dimensions.y / _dimensions.x;
            // var adjustedX = scale.y * aspectRatio;
            //
            // if (adjustedX > scale.x)
            // {
            //     adjustedX = scale.x;
            //     scale.y = adjustedX / aspectRatio; // Recalculate scale.y to maintain the aspect ratio
            // }
            //
            // _field.localScale = new Vector2(adjustedX, scale.y);

            // _field.position = _blockPositionProvider.FieldPosition;
            // _field.localScale = _blockPositionProvider.FieldSize;
        }

        private float CalculateBannerHeight()
        {
            var bannerHeightInDIPs = _addService.GetBannerHeightInPixels();
            var screenDensity = Screen.dpi / 160f;
            var padding = 50f;
            return bannerHeightInDIPs * screenDensity + padding;
        }
    }
}