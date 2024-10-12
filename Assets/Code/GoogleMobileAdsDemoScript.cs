using GoogleMobileAds.Api;
using UnityEngine;

namespace Code
{
    //TODO remove
    public class GoogleMobileAdsDemoScript: MonoBehaviour
    {
        public void Start()
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
            });
            CreateBannerView();
        }
        
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-2135290352531087/1184233585";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-2135290352531087/1184233585";
#else
        private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#endif

        BannerView _bannerView;

        /// <summary>
        /// Creates a 320x50 banner view at top of the screen.
        /// </summary>
        public void CreateBannerView()
        {
            Debug.Log("Creating banner view");

            // If we already have a banner, destroy the old one.
            if (_bannerView != null)
            {
                DestroyAd();
            }
            // Create a 320x50 banner at top of the screen
            _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
            _bannerView.Show();
            // _bannerView.he
        }
        
        /// <summary>
        /// Destroys the banner view.
        /// </summary>
        public void DestroyAd()
        {
            if (_bannerView != null)
            {
                Debug.Log("Destroying banner view.");
                _bannerView.Destroy();
                _bannerView = null;
            }
        }
    }
}