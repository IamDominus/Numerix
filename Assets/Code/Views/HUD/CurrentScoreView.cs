using Code.Providers.SaveLoad;
using Code.Services.Score;
using Code.Utils;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Views.HUD
{
    public class CurrentScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScore;
        private IScoreService _scoreService;
        private IGameSaveProvider _gameSaveProvider;

        [Inject]
        private void Construct(IScoreService scoreService, IGameSaveProvider gameSaveProvider)
        {
            _gameSaveProvider = gameSaveProvider;
            _scoreService = scoreService;
        }

        private void Start()
        {
            UpdateScore(_gameSaveProvider.Data.GetCurrentLevelSaveData().MaxScore);
            _scoreService.Score.ValueChanged += UpdateScore;
        }

        private void UpdateScore(double newScore)
        {
            _currentScore.text = newScore.ToString("N0");
        }

        private void OnDestroy()
        {
            _scoreService.Score.ValueChanged -= UpdateScore;
        }
    }
}