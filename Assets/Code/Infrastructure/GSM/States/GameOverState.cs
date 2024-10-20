using Code.Infrastructure.FSM;
using Code.Services.Input;
using Code.ViewControllers;

namespace Code.Infrastructure.GSM.States
{
    public class GameOverState : IState
    {
        private readonly IInputService _inputService;
        private readonly IViewController _gameOverViewController;

        public GameOverState(IInputService inputService, IViewController gameOverViewController)
        {
            _inputService = inputService;
            _gameOverViewController = gameOverViewController;
        }

        public void Enter()
        {
            _inputService.Disable();
            _gameOverViewController.Show();
        }

        public void Exit()
        {
            _gameOverViewController.Hide();
        }
    }
}