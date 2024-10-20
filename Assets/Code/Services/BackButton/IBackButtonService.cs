using Code.Services.BackButton.Workers;

namespace Code.Services.BackButton
{
    public interface IBackButtonService
    {
        void PushWorker(IBackButtonWorker backButtonWorker);
        void PopWorker();
        void Lock();
        void Unlock();
    }
}