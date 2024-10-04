using Code.Services;
using Code.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Code.Logic
{
    public class BlockView : MonoBehaviour
    {
        public long Value;

        [SerializeField] private TMP_Text _text;

        private IBlockPositionProvider _blockPositionProvider;
        private bool _deleteAfterMove;

        [Inject]
        private void Construct(IBlockPositionProvider blockPositionProvider)
        {
            _blockPositionProvider = blockPositionProvider;
            _text.text = Value.ToString();
        }

        public void Move(Vector2Int position, bool deleteAfterMove)
        {
            _deleteAfterMove = deleteAfterMove;

            var newPosition = _blockPositionProvider.GetBlockInWorldPosition(position);
            transform.DOMove(newPosition, Constants.MOVE_ANIMATION_TIME_SEC).SetEase(Ease.InCubic).OnComplete(OnFinishedMoving);
        }

        public async UniTask Delete()
        {
            if (_deleteAfterMove)
            {
                return;
            }

            await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);
            Destroy(gameObject);
        }

        private void OnFinishedMoving()
        {
            if (_deleteAfterMove)
            {
                Destroy(gameObject);
            }
        }
    }
}