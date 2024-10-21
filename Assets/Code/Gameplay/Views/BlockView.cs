using Code.Gameplay.Providers;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Views
{
    public class BlockView : MonoBehaviour
    {
        public double Value;

        [SerializeField] private TMP_Text _text;

        private IDynamicBoundsProvider _dynamicBoundsProvider;
        private bool _deleteAfterMove;
        private bool _isMoving;

        [Inject]
        private void Construct(IDynamicBoundsProvider dynamicBoundsProvider)
        {
            _dynamicBoundsProvider = dynamicBoundsProvider;
            _text.text = Value.ToString("N0");
        }

        public void Move(Vector2Int position)
        {
            _isMoving = true;
            var newPosition = _dynamicBoundsProvider.GetBlockInWorldPosition(position);
            transform.DOMove(newPosition, Constants.MOVE_ANIMATION_TIME_SEC).SetEase(Ease.InCubic).OnComplete(OnFinishedMoving);
        }

        private void OnFinishedMoving()
        {
            _isMoving = false;
        }

        public void Delete()
        {
            Destroy(gameObject);
        }

        public async UniTask DeleteWithDelay()
        {
            if (_isMoving)
            {
                await UniTask.WaitWhile(() => _isMoving);
            }
            else
            {
                await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);
            }

            Destroy(gameObject);
        }
    }
}