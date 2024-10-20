using Code.Views;
using Code.Views.HUD;
using Code.Views.MainMenu;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public interface IUIFactory
    {
        MainMenuView CreateMainMenu(Transform parent);
        HUDView CreateHUD(Transform parent);
        GameOverView CreateGameOverView(Transform parent);
    }
}