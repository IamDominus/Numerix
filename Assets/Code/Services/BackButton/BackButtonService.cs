using System.Collections.Generic;
using Code.Services.BackButton.Workers;
using UnityEngine;
using Zenject;

namespace Code.Services.BackButton
{
    public class BackButtonService : ITickable, IBackButtonService
    {
        private readonly Stack<IBackButtonWorker> _backButtonWorkers;

        private bool _locked;

        public BackButtonService()
        {
            _backButtonWorkers = new Stack<IBackButtonWorker>();
        }

        public void Tick()
        {
            if (_locked)
                return;

            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape) && _backButtonWorkers.Count > 0)
                _backButtonWorkers.Peek().OnBackButtonClicked();
        }

        public void PushWorker(IBackButtonWorker backButtonWorker)
        {
            _backButtonWorkers.Push(backButtonWorker);
        }

        public void PopWorker()
        {
            if (_backButtonWorkers.Count == 0)
                return;

            _backButtonWorkers.Pop();
        }

        public void Lock()
        {
            _locked = true;
        }

        public void Unlock()
        {
            _locked = false;
        }
    }
}