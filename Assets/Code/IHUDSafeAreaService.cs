using UnityEngine;

namespace Code
{
    public interface IHUDSafeAreaService
    {
        void SetSafeAreaMinAnchor(Vector2 value);
        float SafeAreaTopPadding();
    }
}