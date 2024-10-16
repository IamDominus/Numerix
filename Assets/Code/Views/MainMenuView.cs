using System.Collections.Generic;
using Code.EventSystem;
using Code.EventSystem.Events;
using Code.Providers;
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
        private ISelectedLevelProvider _selectedLevelProvider;

        [Inject]
        private void Construct(IEventBus eventBus, ISelectedLevelProvider selectedLevelProvider)
        {
            _selectedLevelProvider = selectedLevelProvider;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _selectedLevelProvider.Level.ValueChanged += ChangeDimensionsText;
        }

        public void Show(MainMenuViewEntity viewEntity)
        {
            for (var i = 0; i < viewEntity.ResizeFieldButtonViewEntities.Count; i++)
            {
                _resizeFieldButtonViews[i].Show(viewEntity.ResizeFieldButtonViewEntities[i]);
            }

            ChangeDimensionsText(_selectedLevelProvider.Level.Value);
            _playButton.onClick.AddListener(PlayButtonClicked);
            gameObject.SetActive(true);
        }

        private void PlayButtonClicked()
        {
            _eventBus.Invoke<PlayButtonClicked>();
        }

        private void ChangeDimensionsText(Vector2Int newSize)
        {
            _dimensionsText.text = $"{newSize.x} X {newSize.y}";
        }

        public void Hide()
        {
            Unsubscribe();
            gameObject.SetActive(false);
        }

        private void Unsubscribe()
        {
            _playButton.onClick.RemoveListener(PlayButtonClicked);
            _selectedLevelProvider.Level.ValueChanged -= ChangeDimensionsText;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}