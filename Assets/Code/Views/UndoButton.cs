﻿using Code.Gameplay.Facades;
using Code.Services.Input;
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
        private IUndoMoveBlocksFacade _undoMoveBlocksFacade;

        [Inject]
        private void Construct(IInputService inputService, IUndoMoveBlocksFacade undoMoveBlocksFacade)
        {
            _undoMoveBlocksFacade = undoMoveBlocksFacade;
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
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
            _inputService.OnEnabledChanged -= InputServiceOnOnEnabledChanged;
        }
    }
}