namespace Code.Services.Ad
{
    public interface IAdService
    {
        float GetBannerHeightInPixels();
        bool IsInitialized { get; }
        void CreateAndShowBanner();
        void ShowBanner();
        void HideBanner();
        void DestroyBanner();
    }
}