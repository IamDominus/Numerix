using Code.Infrastructure;

namespace Code.Services.Score
{
    public interface IScoreService
    {
        Observable<double> Score { get; }
        void UpdateScore();
    }
}