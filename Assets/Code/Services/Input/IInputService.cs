using System;
using Code.Enums;

namespace Code.Services.Input
{
    public interface IInputService
    {
        event Action<DragDirection> OnDragged;
        event Action<bool> OnEnabledChanged;
        void Enable();
        void Disable();
    }
}