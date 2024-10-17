using System;
using Code.Data;
using Code.Gameplay.Providers;
using Code.Infrastructure;
using Code.Providers.SaveLoad;
using Code.Services.SaveLoad;
using Code.Utils;
using Zenject;

namespace Code.Services.Score
{
    public class ScoreService : IScoreService, IGameSaveWriter, IInitializable, IDisposable
    {
        private const double DELTA = 0.9;

        public Observable<double> Score { get; }

        private readonly IBlocksProvider _blocksProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public ScoreService(IBlocksProvider blocksProvider, ISaveLoadRegistry saveLoadRegistry)
        {
            _blocksProvider = blocksProvider;
            _saveLoadRegistry = saveLoadRegistry;
            Score = new Observable<double>(0);
        }

        public void Initialize()
        {
            _saveLoadRegistry.RegisterSaveWriter(this);
        }

        public void UpdateScore()
        {
            var currentScore = GetCurrentScore();

            if (Math.Abs(Score.Value - currentScore) > DELTA)
            {
                Score.Value = currentScore;
            }
        }

        public void SaveGameData(GameSaveData data)
        {
            var levelData = data.GetCurrentLevelSaveData();
            levelData.MaxScore = Math.Max(Score.Value, levelData.MaxScore);
        }

        private double GetCurrentScore()
        {
            var tempScore = 0d;

            for (var x = 0; x < _blocksProvider.Blocks.GetLength(0); x++)
            {
                for (var y = 0; y < _blocksProvider.Blocks.GetLength(1); y++)
                {
                    var block = _blocksProvider.Blocks[x, y];

                    if (block != null)
                    {
                        tempScore += block.Model.Value;
                    }
                }
            }

            return tempScore;
        }

        public void Dispose()
        {
            _saveLoadRegistry.UnregisterSaveWriter(this);
        }
    }
}