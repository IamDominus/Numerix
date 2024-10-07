using Code.EventSystem;
using Code.EventSystem.Events;
using Code.ViewEntities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class ResizeFieldButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectedColor;

        private IEventBus _eventBus;
        private ResizeFieldButtonViewEntity _viewEntity;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Show(ResizeFieldButtonViewEntity viewEntity)
        {
            _viewEntity = viewEntity;
            _button.interactable = viewEntity.Interactable;
            _button.onClick.AddListener(OnButtonClicked);
            _eventBus.Subscribe<ResizeFieldEvent>(OnResizeButtonClicked);
        }

        private void OnButtonClicked()
        {
            var resizeFieldEvent = new ResizeFieldEvent()
            {
                X = _viewEntity.X,
                Y = _viewEntity.Y
            };
            _eventBus.Invoke(resizeFieldEvent);
        }

        private void OnResizeButtonClicked(ResizeFieldEvent payload)
        {
            if (_viewEntity.Y > payload.Y || _viewEntity.X > payload.X)
            {
                _buttonImage.color = _normalColor;
            }
            else
            {
                _buttonImage.color = _selectedColor;
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ResizeFieldEvent>(OnResizeButtonClicked);
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}