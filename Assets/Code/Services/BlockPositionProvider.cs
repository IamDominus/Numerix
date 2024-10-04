using System.Collections.Generic;
using Code.Providers;
using Code.StaticData;
using Code.Utils;
using UnityEngine;
using Zenject;

namespace Code.Services
{
    public class BlockPositionProvider : IBlockPositionProvider, IInitializable
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly System.Random _random;
        private LevelStaticData _levelData;

        public BlockPositionProvider(IStaticDataProvider staticDataProvider)
        {
            _staticDataProvider = staticDataProvider;
            _random = new System.Random();
        }

        public void Initialize()
        {
            _levelData = _staticDataProvider.GetLevelStaticData(Constants.DIMENSIONS);
        }

        public Vector2 GetBlockInWorldPosition(int x, int y)
        {
            var totalXSize = _levelData.CellSize.x * _levelData.Dimensions.y;
            var xOffset = (_levelData.FieldSize.x - totalXSize) / (_levelData.Dimensions.y + 1);

            var totalYSize = _levelData.CellSize.y * _levelData.Dimensions.x;
            var yOffset = (_levelData.FieldSize.y - totalYSize) / (_levelData.Dimensions.x + 1);

            var fieldTopLeft = new Vector2(_levelData.FieldSize.x / -2, _levelData.FieldSize.y / 2);

            var xWorldOffset = y * (_levelData.CellSize.x + xOffset);
            var xWorld = fieldTopLeft.x + _levelData.CellSize.x / 2 + xOffset + xWorldOffset;

            var yWorldOffset = x * (_levelData.CellSize.y + yOffset);
            var yWorld = fieldTopLeft.y - _levelData.CellSize.y / 2 - yOffset - yWorldOffset;

            return new Vector2(xWorld, yWorld);
        }

        public Vector2 GetBlockInWorldPosition(Vector2Int position)
        {
            return GetBlockInWorldPosition(position.x, position.y);
        }

        public bool TryGetRandomPosition<T>(T[,] array, out Vector2Int position)
        {
            var nullIndexes = new List<Vector2Int>();

            for (var x = 0; x < array.GetLength(0); x++)
            {
                for (var y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y] == null)
                    {
                        nullIndexes.Add(new Vector2Int(x, y));
                    }
                }
            }

            if (nullIndexes.Count > 0)
            {
                position = nullIndexes[_random.Next(nullIndexes.Count)];
                return true;
            }

            position = VectorUtils.EMPTY;
            return false;
        }
    }
}