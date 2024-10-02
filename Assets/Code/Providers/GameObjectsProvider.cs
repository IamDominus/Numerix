using UnityEngine;

namespace Code.Providers
{
    public class GameObjectsProvider : MonoBehaviour, IGameObjectsProvider
    {
        public Transform CellsParent => _cellsParent;

        [SerializeField] private Transform _cellsParent;
    }
}