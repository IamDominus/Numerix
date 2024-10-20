using UnityEngine;

namespace Code.ViewControllers.HUD
{
    public interface IHUDViewController
    {
        void Show();
        float HUDBottomPoint();
        void UpdateMinAnchor(Vector2 minAnchor);
    }
}