using UnityEngine;

namespace Code.Providers.GameObject
{
    public interface ILevelObjectsProvider
    {
        Transform CellsParent { get; }
    }
}