using System.Collections.Generic;
using Code.Logic;
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

        public bool TryGetBlockRandomPosition(Block[,] blocks, out Vector2Int position)
        {
            var nullIndices = new List<Vector2Int>();

            for (var x = 0; x < blocks.GetLength(0); x++)
            {
                for (var y = 0; y < blocks.GetLength(1); y++)
                {
                    if (blocks[x, y] == null)
                    {
                        nullIndices.Add(new Vector2Int(x, y));
                    }
                }
            }

            if (nullIndices.Count > 0)
            {
                position = nullIndices[_random.Next(nullIndices.Count)];
                return true;
            }

            position = Vector2IntUtils.EMPTY;
            return false;
        }
    }
}