using UnityEngine;

namespace Code.Services.HUD
{
    public interface IHUDSafeAreaService
    {
        void SetSafeAreaMinAnchor(Vector2 value);
        float SafeAreaTopPadding();
    }
}