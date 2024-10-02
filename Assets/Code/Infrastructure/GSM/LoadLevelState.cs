using Code.Infrastructure.FSM;

namespace Code.Infrastructure.GSM
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLevelLoaded);
        }

        private void OnLevelLoaded()
        {
            _gameStateMachine.Enter<ConstructLevelState>();
        }

        public void Exit()
        {
        }
    }
}