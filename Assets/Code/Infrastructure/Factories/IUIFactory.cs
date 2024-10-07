using Code.Views;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public interface IUIFactory
    {
        MainMenuView CreateMainMenu(Transform parent);
    }
}