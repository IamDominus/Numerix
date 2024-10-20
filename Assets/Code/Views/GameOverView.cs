using Code.Infrastructure.GSM;
using Code.Infrastructure.GSM.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Views
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _button.onClick.AddListener(ContinueButtonClicked);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ContinueButtonClicked()
        {
            _gameStateMachine.Enter<RestartLevelState>();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ContinueButtonClicked);
        }
    }
}