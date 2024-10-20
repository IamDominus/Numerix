using UnityEngine;

namespace Code.Services.BackButton.Workers
{
    public class MainMenuBackButtonWorker : IBackButtonWorker
    {
        public void OnBackButtonClicked()
        {
            Application.Quit();
        }
    }
}