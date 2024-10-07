using Code.Views;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class UIFactory : IUIFactory, IInitializable
    {
        private const string MAIN_MENU_PREFAB_PATH = "Prefabs/Menu/MainMenu";

        private readonly DiContainer _diContainer;
        
        private MainMenuView _mainMenuPrefab;

        public UIFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _mainMenuPrefab = Resources.Load<MainMenuView>(MAIN_MENU_PREFAB_PATH);
        }

        public MainMenuView CreateMainMenu(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<MainMenuView>(_mainMenuPrefab, parent);
        }
    }
}