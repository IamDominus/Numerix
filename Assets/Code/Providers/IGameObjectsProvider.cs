using UnityEngine;

namespace Code.Providers
{
    public interface IGameObjectsProvider
    {
        Transform CellsParent { get; }
    }
}