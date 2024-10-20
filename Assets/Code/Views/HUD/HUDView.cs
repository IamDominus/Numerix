using Code.Enums;
using Code.Gameplay.Facades;
using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.Payloads;
using Code.Infrastructure.GSM.States;
using Code.Providers.SaveLoad;
using Code.Services.Input;
using Code.Services.Score;
using Code.Utils;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views.HUD
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _maxScoreText;
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _undoButton;

        private IScoreService _scoreService;
        private IGameSaveProvider _gameSaveProvider;
        private GameStateMachine _gameStateMachine;
        private IInputService _inputService;
        private IUndoMoveBlocksFacade _undoMoveBlocksFacade;

        private double _maxScore;

        [Inject]
        private void Construct(IScoreService scoreService, IGameSaveProvider gameSaveProvider, GameStateMachine gameStateMachine, IInputService inputService,
            IUndoMoveBlocksFacade undoMoveBlocksFacade)
        {
            _scoreService = scoreService;
            _gameSaveProvider = gameSaveProvider;
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
            _undoMoveBlocksFacade = undoMoveBlocksFacade;
        }

        private void Start()
        {
            InitializeScores();
            _scoreService.Score.ValueChanged += UpdateScore;

            _homeButton.onClick.AddListener(OnHomeButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _undoButton.onClick.AddListener(OnUndoButtonClicked);

            _inputService.OnEnabledChanged += OnInputEnabledChanged;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void InitializeScores()
        {
            var currentLevelData = _gameSaveProvider.Data.GetCurrentLevelSaveData();
            UpdateScore(_scoreService.Score.Value);
            UpdateMaxScore(currentLevelData.MaxScore);
        }

        private void UpdateScore(double newScore)
        {
            _currentScore.text = newScore.ToString(Constants.SCORE_FORMAT);
            if (_maxScore < newScore)
            {
                UpdateMaxScore(newScore);
            }
        }

        private void UpdateMaxScore(double newMaxScore)
        {
            _maxScore = newMaxScore;
            _maxScoreText.text = _maxScore.ToString(Constants.SCORE_FORMAT);
        }

        private void OnHomeButtonClicked()
        {
            var payload = new LoadScenePayload
            {
                SceneName = SceneName.MainMenu,
                Callback = () => _gameStateMachine.Enter<MainMenuState>()
            };
            _gameStateMachine.Enter<LoadSceneState, LoadScenePayload>(payload);
        }

        private void OnRestartButtonClicked()
        {
            _gameStateMachine.Enter<RestartLevelState>();
        }

        private void OnUndoButtonClicked()
        {
            _undoMoveBlocksFacade.UndoMoveBlocks().Forget();
        }

        private void OnInputEnabledChanged(bool isEnabled)
        {
            _restartButton.interactable = isEnabled;
            _undoButton.interactable = isEnabled;
        }

        private void OnDestroy()
        {
            _homeButton.onClick.RemoveListener(OnHomeButtonClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _undoButton.onClick.RemoveListener(OnUndoButtonClicked);

            _scoreService.Score.ValueChanged -= UpdateScore;
            _inputService.OnEnabledChanged -= OnInputEnabledChanged;
        }
    }
}