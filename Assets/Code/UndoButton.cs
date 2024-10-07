using Code.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code
{
    public class UndoButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private PlayerTurnService _playerTurnService;
        private IInputService _inputService;

        [Inject]
        private void Construct(PlayerTurnService playerTurnService, IInputService inputService)
        {
            _playerTurnService = playerTurnService;
            _inputService = inputService;
        }

        private void Awake()
        {
            _button.onClick.AddListener(Call);
            _inputService.OnEnabledChanged += InputServiceOnOnEnabledChanged;
        }

        private void InputServiceOnOnEnabledChanged(bool isEnabled)
        {
            _button.interactable = isEnabled;
        }

        private void Call()
        {
            _playerTurnService.Undo();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Call);
            _inputService.OnEnabledChanged -= InputServiceOnOnEnabledChanged;
        }
    }
}