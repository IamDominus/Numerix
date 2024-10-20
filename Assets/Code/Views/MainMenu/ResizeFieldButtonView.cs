using Code.Providers;
using Code.Services.SaveLoad;
using Code.ViewEntities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views.MainMenu
{
    public class ResizeFieldButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectedColor;

        private ResizeFieldButtonViewEntity _viewEntity;
        private ISelectedLevelProvider _selectedLevelProvider;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISelectedLevelProvider selectedLevelProvider, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _selectedLevelProvider = selectedLevelProvider;
        }

        public void Show(ResizeFieldButtonViewEntity viewEntity)
        {
            _viewEntity = viewEntity;
            _button.interactable = viewEntity.Interactable;
            ChangeButtonColor(_selectedLevelProvider.Level.Value);
            
            _button.onClick.AddListener(OnButtonClicked);
            _selectedLevelProvider.Level.ValueChanged += ChangeButtonColor;
        }

        private void OnButtonClicked()
        {
            _selectedLevelProvider.Level.Value = new Vector2Int(_viewEntity.X, _viewEntity.Y);
            _saveLoadService.SaveGameData();
        }

        private void ChangeButtonColor(Vector2Int selectedButton)
        {
            if (_viewEntity.Y > selectedButton.y || _viewEntity.X > selectedButton.x)
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
            _selectedLevelProvider.Level.ValueChanged -= ChangeButtonColor;
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}