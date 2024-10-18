using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameStateMachine _gameStateMachine;
        private IInputService _inputService;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, IInputService inputService)
        {
            _inputService = inputService;
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
            _inputService.OnEnabledChanged += OnInputEnabledChanged;
        }

        private void OnInputEnabledChanged(bool isEnabled)
        {
            _button.interactable = isEnabled;
        }

        private void OnButtonClicked()
        {
            _gameStateMachine.Enter<RestartLevelState>();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _inputService.OnEnabledChanged -= OnInputEnabledChanged;
        }
    }
}