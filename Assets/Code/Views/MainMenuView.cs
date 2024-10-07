using System.Collections.Generic;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.ViewEntities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private List<ResizeFieldButtonView> _resizeFieldButtonViews;
        [SerializeField] private Button _playButton;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Show(MainMenuViewEntity viewEntity)
        {
            for (var i = 0; i < viewEntity.ResizeFieldButtonViewEntities.Count; i++)
            {
                _resizeFieldButtonViews[i].Show(viewEntity.ResizeFieldButtonViewEntities[i]);
            }

            _playButton.onClick.AddListener(PlayButtonClicked);
            gameObject.SetActive(true);
        }

        private void PlayButtonClicked()
        {
            _eventBus.Invoke<PlayButtonClicked>();
        }

        public void Hide()
        {
            _playButton.onClick.RemoveListener(PlayButtonClicked);
            gameObject.SetActive(false);
        }
    }
}