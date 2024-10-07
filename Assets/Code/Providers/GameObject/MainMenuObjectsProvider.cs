using UnityEngine;

namespace Code.Providers.GameObject
{
    public class MainMenuObjectsProvider : MonoBehaviour, IMainMenuObjectsProvider
    {
        public Transform MainMenuParentParent => _mainMenuParent;

        [SerializeField] private Transform _mainMenuParent;
    }
}