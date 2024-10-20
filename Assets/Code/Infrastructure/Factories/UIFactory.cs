﻿using Code.Views;
using Code.Views.HUD;
using Code.Views.MainMenu;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class UIFactory : IUIFactory, IInitializable
    {
        private const string MAIN_MENU_PREFAB_PATH = "UI/MenuMenu/MainMenu";
        private const string HUD_PREFAB_PATH = "UI/HUD/HUD";
        private const string GAME_OVER_PREFAB_PATH = "UI/GameOver";

        private readonly DiContainer _diContainer;

        private MainMenuView _mainMenuPrefab;
        private HUDView _hudPrefab;
        private GameOverView _gameOverViewPrefab;

        public UIFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _mainMenuPrefab = Resources.Load<MainMenuView>(MAIN_MENU_PREFAB_PATH);
            _hudPrefab = Resources.Load<HUDView>(HUD_PREFAB_PATH);
            _gameOverViewPrefab = Resources.Load<GameOverView>(GAME_OVER_PREFAB_PATH);
        }

        public MainMenuView CreateMainMenu(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<MainMenuView>(_mainMenuPrefab, parent);
        }

        public HUDView CreateHUD(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<HUDView>(_hudPrefab, parent);
        }

        public GameOverView CreateGameOverView(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<GameOverView>(_gameOverViewPrefab, parent);
        }
    }
}