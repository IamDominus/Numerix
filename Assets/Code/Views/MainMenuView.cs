using System.Collections.Generic;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.ViewEntities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private List<ResizeFieldButtonView> _resizeFieldButtonViews;
        [SerializeField] private Button _playButton;
        [SerializeField] private TMP_Text _dimensionsText;
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

        private void OnResizeField(ResizeFieldEvent payload)
        {
            _dimensionsText.text = $"{payload.X} X {payload.Y}";
        }

        public void Hide()
        {
            Unsubscribe();
            gameObject.SetActive(false);
        }

        private void Unsubscribe()
        {
            _playButton.onClick.RemoveListener(PlayButtonClicked);
            _eventBus.Unsubscribe<ResizeFieldEvent>(OnResizeField);
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}