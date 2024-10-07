using Code.EventSystem;
using Code.EventSystem.Events;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code
{
    public class HomeButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _eventBus.Invoke<HomeButtonClicked>();
        }
    }
}