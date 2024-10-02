using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Runners
{
    public class GameRunner : MonoBehaviour
    {
        private void Awake()
        {
            var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            if (gameBootstrapper == null)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}