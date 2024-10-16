using System.Collections.Generic;
using Code.Infrastructure.Factories;
using Code.Providers.GameObject;
using Code.ViewEntities;
using Code.Views;

namespace Code.ViewControllers
{
    public class MainMenuViewController : IViewController
    {
        private readonly IUIFactory _uiFactory;
        private readonly IMainMenuObjectsProvider _objectsProvider;
        private MainMenuView _mainMenuView;

        public MainMenuViewController(IUIFactory uiFactory, IMainMenuObjectsProvider objectsProvider)
        {
            _uiFactory = uiFactory;
            _objectsProvider = objectsProvider;
        }

        public void Show()
        {
            _mainMenuView ??= _uiFactory.CreateMainMenu(_objectsProvider.MainMenuParentParent);
            var mainMenuViewEntity = GetMainMenuViewEntity();
            _mainMenuView.Show(mainMenuViewEntity);
        }

        public void Hide()
        {
            _mainMenuView.Hide();
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