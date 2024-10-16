using System;
using Code.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services.Input
{
    public class InputService : MonoBehaviour, IInputService, IDragHandler, IEndDragHandler
    {
        public event Action<DragDirection> OnDragged;
        public event Action<bool> OnEnabledChanged;

        private bool _isEnabled;

        public void Enable()
        {
            _isEnabled = true;
            OnEnabledChanged?.Invoke(_isEnabled);
        }

        public void Disable()
        {
            _isEnabled = false;
            OnEnabledChanged?.Invoke(_isEnabled);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isEnabled == false)
            {
                return;
            }

            var dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
            var dragDirection = GetDragDirection(dragVectorDirection);
            OnDragged?.Invoke(dragDirection);
        }

        //It must be implemented otherwise IEndDragHandler won't work
        public void OnDrag(PointerEventData eventData)
        {
        }

        private DragDirection GetDragDirection(Vector3 dragVector)
        {
            var positiveX = Mathf.Abs(dragVector.x);
            var positiveY = Mathf.Abs(dragVector.y);

            DragDirection draggedDir;

            if (positiveX > positiveY)
            {
                draggedDir = (dragVector.x > 0) ? DragDirection.Right : DragDirection.Left;
            }
            else
            {
                draggedDir = (dragVector.y > 0) ? DragDirection.Up : DragDirection.Down;
            }

            return draggedDir;
        }
    }
}