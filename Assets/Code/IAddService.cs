namespace Code
{
    public interface IAddService
    {
        float GetBannerHeightInPixels();
        bool IsInitialized { get; }
        void CreateAndShowBanner();
        void ShowBanner();
        void HideBanner();
        void DestroyBanner();
    }
}