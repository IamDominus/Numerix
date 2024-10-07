using UnityEngine;

namespace Code.Providers.GameObject
{
    public class LevelObjectsProvider : MonoBehaviour, ILevelObjectsProvider
    {
        public Transform CellsParent => _cellsParent;

        [SerializeField] private Transform _cellsParent;
    }
}