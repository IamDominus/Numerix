using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Logic
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        public long Value;
        
        private bool _merged;
        private bool _moving;

        public void SetValue(long value)
        {
            Value = value;
            _text.text = Value.ToString();
        }

        public void MoveTo(Vector2 position, bool deleteAfterMove)
        {
            _merged = deleteAfterMove;
            _moving = true;
            transform.DOMove(position, Constants.MOVE_ANIMATION_TIME_SEC).OnComplete(OnFinishedMoving);
        }

        public void Delete()
        {
            if (_moving == false)
            {
                Destroy(gameObject, Constants.MOVE_ANIMATION_TIME_SEC);
            }
            else
            {
                _merged = true;
            }
        }

        private void OnFinishedMoving()
        {
            if (_merged)
            {
                Destroy(gameObject);
            }

            _moving = false;
        }
    }
}