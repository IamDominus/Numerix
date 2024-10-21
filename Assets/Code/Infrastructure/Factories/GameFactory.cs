using System.Collections.Generic;
using System.Linq;
using Code.Gameplay;
using Code.Gameplay.Views;
using Code.Providers.GameObject;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class GameFactory : IInitializable, IGameFactory
    {
        private const string CELL_PREFAB_PATH = "Prefabs/FieldAndCell/Cell";
        private const string BLOCKS_FOLDER_PREFAB_PATH = "Prefabs/Blocks";

        private readonly IInstantiator _instantiator;
        private readonly Transform _parent;

        private Cell _cellPrefab;
        private Dictionary<double, BlockView> _blockViews;

        public GameFactory(IInstantiator instantiator, ILevelObjectsProvider levelObjectsProvider)
        {
            _instantiator = instantiator;
            _parent = levelObjectsProvider.CellsParent;
        }

        public void Initialize()
        {
            _cellPrefab = Resources.Load<Cell>(CELL_PREFAB_PATH);
            _blockViews = Resources.LoadAll<BlockView>(BLOCKS_FOLDER_PREFAB_PATH).ToDictionary(x => x.Value, x => x);
        }

        public void CreateCell(Vector3 position, Vector2 size)
        {
            var go = _instantiator.InstantiatePrefab(_cellPrefab, position, Quaternion.identity, _parent);
            go.transform.localScale = size;
        }

        public BlockView CreateBlockView(Vector2 position, Vector2 size, double value)
        {
            var blockView = _instantiator.InstantiatePrefabForComponent<BlockView>(_blockViews[value], position, Quaternion.identity, _parent);
            blockView.transform.localScale = size;
            return blockView;
        }

        public Block CreateBlock(BlockModel blockModel, Vector2 position, Vector2 size, double value)
        {
            var blockView = CreateBlockView(position, size, value);
            var block = _instantiator.Instantiate<Block>();
            block.Initialize(blockModel, blockView);
            return block;
        }
    }
}