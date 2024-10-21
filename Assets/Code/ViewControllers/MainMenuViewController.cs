using System.Collections.Generic;
using Code.Enums;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Infrastructure.Factories;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Infrastructure.GSM.States;
using Code.Providers.GameObject;
using Code.ViewEntities;
using Code.Views.MainMenu;

namespace Code.ViewControllers
{
    public class MainMenuViewController : IViewController
    {
        private readonly IUIFactory _uiFactory;
        private readonly IMainMenuObjectsProvider _objectsProvider;
        private readonly IEventBus _eventBus;
        private readonly GameStateMachine _gameStateMachine;

        private MainMenuView _mainMenuView;

        public MainMenuViewController(IUIFactory uiFactory, IMainMenuObjectsProvider objectsProvider, GameStateMachine gameStateMachine, EventBus eventBus)
        {
            _uiFactory = uiFactory;
            _objectsProvider = objectsProvider;
            _gameStateMachine = gameStateMachine;
            _eventBus = eventBus;
        }

        public void Show()
        {
            _mainMenuView ??= _uiFactory.CreateMainMenu(_objectsProvider.MainMenuParentParent);
            var mainMenuViewEntity = GetMainMenuViewEntity();
            _mainMenuView.Show(mainMenuViewEntity);
            _eventBus.Subscribe<PlayButtonClicked>(OnPlayButtonClicked);
        }

        public void Hide()
        {
            _eventBus.Unsubscribe<PlayButtonClicked>(OnPlayButtonClicked);
            _mainMenuView.Hide();
        }

        private void OnPlayButtonClicked()
        {
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.Level,
                Callback = () => _gameStateMachine.Enter<ConstructLevelState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }

        private MainMenuViewEntity GetMainMenuViewEntity()
        {
            var resizeFieldButtonViewEntities = new List<ResizeFieldButtonViewEntity>();

            for (var x = 1; x <= Constants.MAX_DIMENSIONS.x; x++)
            {
                for (var y = 1; y <= Constants.MAX_DIMENSIONS.y; y++)
                {
                    resizeFieldButtonViewEntities.Add(new ResizeFieldButtonViewEntity()
                    {
                        X = x,
                        Y = y,
                        Interactable = x != 1 && y != 1
                    });
                }
            }

            return new MainMenuViewEntity()
            {
                ResizeFieldButtonViewEntities = resizeFieldButtonViewEntities
            };
        }
    }
}