using System.Collections.Generic;
using System.Linq;
using Code.Logic;
using Code.Views;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class GameFactory : IInitializable, IGameFactory
    {
        private const string CELL_PREFAB_PATH = "Cell";
        private const string BLOCKS_FOLDER_PREFAB_PATH = "Blocks";

        private readonly DiContainer _diContainer;
        private Cell _cellPrefab;
        private Dictionary<long, BlockView> _blockViews;

        public GameFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            _cellPrefab = Resources.Load<Cell>(CELL_PREFAB_PATH);
            _blockViews = Resources.LoadAll<BlockView>(BLOCKS_FOLDER_PREFAB_PATH).ToDictionary(x => x.Value, x => x);
        }

        public void CreateCell(Vector3 position, Transform parent, Vector2 size)
        {
            var go = _diContainer.InstantiatePrefab(_cellPrefab, position, Quaternion.identity, parent);
            go.transform.localScale = size;
        }

        public BlockView CreateBlockView(Vector2 position, Transform parent, Vector2 size, long value)
        {
            var blockView = _diContainer.InstantiatePrefabForComponent<BlockView>(_blockViews[value], position, Quaternion.identity, parent);
            blockView.transform.localScale = size;
            return blockView;
        }

        public Block CreateBlock(BlockModel blockModel, Vector2 position, Transform parent, Vector2 size, long value)
        {
            var blockView = CreateBlockView(position, parent, size, value);
            var block = _diContainer.Instantiate<Block>();
            block.Initialize(blockModel, blockView);
            return block;
        }
    }
}