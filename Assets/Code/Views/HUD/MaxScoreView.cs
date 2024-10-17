using System;
using Code.Providers.SaveLoad;
using Code.Services.Score;
using Code.Utils;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Views.HUD
{
    public class MaxScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        private IGameSaveProvider _gameSaveProvider;
        private IScoreService _scoreService;
        private double _maxScore;

        [Inject]
        private void Construct(IGameSaveProvider gameSaveProvider, IScoreService scoreService)
        {
            _scoreService = scoreService;
            _gameSaveProvider = gameSaveProvider;
        }

        private void Start()
        {
            var levelData = _gameSaveProvider.Data.GetCurrentLevelSaveData();
            UpdateMaxScore(levelData.MaxScore);
            _scoreService.Score.ValueChanged += OnScoreChanged;
        }

        private void UpdateMaxScore(double newMaxScore)
        {
            _maxScore = newMaxScore;
            _score.text = _maxScore.ToString("N0");
        }

        private void OnScoreChanged(double newScore)
        {
            if (_maxScore < newScore)
            {
                UpdateMaxScore(newScore);
            }
        }

        private void OnDestroy()
        {
            _scoreService.Score.ValueChanged -= OnScoreChanged;
        }
    }
}