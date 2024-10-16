using UnityEngine;

namespace Code.Providers.GameObject
{
    public interface ILevelObjectsProvider
    {
        Transform CellsParent { get; }
        Camera MainCamera { get; }
        Canvas MainCanvas { get; }
        RectTransform SafeArea { get; }
    }
}