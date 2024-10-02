using System;
using Code.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Services
{
    public class InputService : MonoBehaviour, IInputService, IDragHandler, IEndDragHandler
    {
        public event Action<DragDirection> OnDragged;

        private bool _isEnabled;

        public void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
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