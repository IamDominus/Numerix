using System;
using Code.Gameplay.Providers;

namespace Code.Providers
{
    public class RandomBlockValueProvider : IRandomBlockValueProvider
    {
        private readonly IBlocksProvider _blocksProvider;
        private readonly Random _random;

        public RandomBlockValueProvider(IBlocksProvider blocksProvider, Random random)
        {
            _blocksProvider = blocksProvider;
            _random = random;
        }

        public double GetRandomValue()
        {
            var nullIndexes = GetNullIndexesCount();

            if (nullIndexes == 1)
            {
                return 2;
            }

            return _random.Next(0, 2) == 0 ? 2 : 4;
        }

        private int GetNullIndexesCount()
        {
            var nullIndexes = 0;

            for (var x = 0; x < _blocksProvider.Blocks.GetLength(0); x++)
            {
                for (var y = 0; y < _blocksProvider.Blocks.GetLength(1); y++)
                {
                    if (_blocksProvider.Blocks[x, y] == null)
                    {
                        nullIndexes++;
                    }
                }
            }

            return nullIndexes;
        }
    }
}