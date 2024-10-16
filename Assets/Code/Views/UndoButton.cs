using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Gameplay.Facades;
using Code.Gameplay.Features;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class UndoButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IInputService _inputService;
        private IEventBus _eventBus;
        private IUndoMoveBlocksFacade _undoMoveBlocksFacade;
//TODO remove event bus call
        [Inject]
        private void Construct(IInputService inputService, IEventBus eventBus, IUndoMoveBlocksFacade undoMoveBlocksFacade)
        {
            _undoMoveBlocksFacade = undoMoveBlocksFacade;
            _eventBus = eventBus;
            _inputService = inputService;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
            _inputService.OnEnabledChanged += InputServiceOnOnEnabledChanged;
        }

        private void InputServiceOnOnEnabledChanged(bool isEnabled)
        {
            _button.interactable = isEnabled;
        }

        private void OnButtonClicked()
        {
            _undoMoveBlocksFacade.UndoMoveBlocks().Forget();
            _eventBus.Invoke<UndoButtonClicked>();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _inputService.OnEnabledChanged -= InputServiceOnOnEnabledChanged;
        }
    }
}