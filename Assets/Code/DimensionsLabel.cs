using Code.EventSystem;
using Code.EventSystem.Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code
{
    public class DimensionsLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _eventBus.Subscribe<ResizeFieldEvent>(OnResizeField);
        }

        private void OnResizeField(ResizeFieldEvent payload)
        {
            _text.text = $"{payload.X} X {payload.Y}";
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ResizeFieldEvent>(OnResizeField);
        }
    }
}