using Code.Enums;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Infrastructure.GSM.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            var payload = new LoadScenePayload()
            {
                SceneName = SceneName.MainMenu,
                Callback = () => _gameStateMachine.Enter<MainMenuState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}