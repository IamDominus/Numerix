using System;
using Code.Enums;

namespace Code.Services
{
    public interface IInputService
    {
        event Action<DragDirection> OnDragged;
        void SetEnabled(bool isEnabled);
    }
}