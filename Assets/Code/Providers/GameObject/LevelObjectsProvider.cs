using UnityEngine;

namespace Code.Providers.GameObject
{
    public class LevelObjectsProvider : MonoBehaviour, ILevelObjectsProvider
    {
        public Transform CellsParent => _cellsParent;
        public Camera MainCamera => _mainCamera;
        public Canvas MainCanvas => _mainCanvas;

        [SerializeField] private Transform _cellsParent;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Canvas _mainCanvas;
    }
}