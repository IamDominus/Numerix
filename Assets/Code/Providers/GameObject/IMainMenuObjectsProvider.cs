using UnityEngine;

namespace Code.Providers.GameObject
{
    public interface IMainMenuObjectsProvider
    {
        Transform MainMenuParentParent { get; }
    }
}