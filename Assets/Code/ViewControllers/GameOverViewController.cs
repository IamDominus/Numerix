using Code.Infrastructure.Factories;
using Code.Providers.GameObject;
using Code.Views;

namespace Code.ViewControllers
{
    public class GameOverViewController : IViewController
    {
        private readonly IUIFactory _uiFactory;
        private readonly ILevelObjectsProvider _objectsProvider;

        private GameOverView _gameOverView;

        public GameOverViewController(IUIFactory uiFactory, ILevelObjectsProvider objectsProvider)
        {
            _uiFactory = uiFactory;
            _objectsProvider = objectsProvider;
        }

        public void Show()
        {
            _gameOverView ??= _uiFactory.CreateGameOverView(_objectsProvider.MainCanvas.transform);
            _gameOverView.Show();
        }

        public void Hide()
        {
            _gameOverView.Hide();
        }
    }
}