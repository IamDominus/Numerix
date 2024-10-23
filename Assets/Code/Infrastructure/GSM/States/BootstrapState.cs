using Code.Infrastructure.FSM;
using Code.Views.LoadingCurtain;
using DG.Tweening;
using UnityEngine;

namespace Code.Infrastructure.GSM.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILoadingCurtainView _loadingCurtainView;

        public BootstrapState(GameStateMachine gameGameStateMachine, ILoadingCurtainView loadingCurtainView)
        {
            _gameStateMachine = gameGameStateMachine;
            _loadingCurtainView = loadingCurtainView;
        }

        public void Enter()
        {
            _loadingCurtainView.Show();
            Application.targetFrameRate = 60;
            DOTween.Init();
            _gameStateMachine.Enter<LoadGameState>();
        }

        public void Exit()
        {
        }
    }
}