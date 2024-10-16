using GoogleMobileAds.Api;
using Zenject;

namespace Code.Services.Ad
{
    public class AdService : IInitializable, IAdService
    {
        private static readonly AdSize BANNER_SIZE = AdSize.Banner;
#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-2135290352531087/1184233585";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-2135290352531087/1184233585";
#else
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#endif

        public bool IsInitialized => _isInitialized;

        private BannerView _bannerView;
        private bool _isInitialized;

        public void Initialize()
        {
            MobileAds.Initialize((InitializationStatus initStatus) => { _isInitialized = true; });
        }

        public float GetBannerHeightInPixels()
        {
            if (_bannerView != null)
            {
                return BANNER_SIZE.Height;
            }

            return 0;
        }

        public void CreateAndShowBanner()
        {
            _bannerView?.Destroy();
            _bannerView = new BannerView(_adUnitId, BANNER_SIZE, AdPosition.Bottom);
            ShowBanner();
        }

        public void ShowBanner()
        {
            _bannerView?.Show();
        }

        public void HideBanner()
        {
            _bannerView?.Hide();
        }

        public void DestroyBanner()
        {
            if (_bannerView != null)
            {
                _bannerView.Destroy();
                _bannerView = null;
            }
        }
    }
}