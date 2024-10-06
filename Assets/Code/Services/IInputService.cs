using System;
using Code.Enums;

namespace Code.Services
{
    public interface IInputService
    {
        event Action<DragDirection> OnDragged;
        event Action<bool> OnEnabledChanged;
        void Enable();
        void Disable();
    }
}