using Code.Gameplay.Providers;
using Code.Services;
using Code.Services.Spawn;
using Code.Utils;

namespace Code.Gameplay.Features
{
    public class UndoMoveBlocksFeature : IUndoMoveBlocksFeature
    {
        private Block[,] Blocks => _levelDataProvider.Blocks;

        private readonly ISpawnService _spawnService;
        private readonly ILevelDataProvider _levelDataProvider;

        public UndoMoveBlocksFeature(ISpawnService spawnService, ILevelDataProvider levelDataProvider)
        {
            _spawnService = spawnService;
            _levelDataProvider = levelDataProvider;
        }

        public void UndoMoveBlocks()
        {
            var oldModels = _levelDataProvider.PopPreviousTurnBlockModels();
            var undoDirection = _levelDataProvider.PopPreviousTurnMoveDirection() * -1;

            var xMax = Blocks.GetLength(0);
            var yMax = Blocks.GetLength(1);

            for (var x = undoDirection.x > 0 ? xMax - 1 : 0; x >= 0 && x < xMax; x += undoDirection.x > 0 ? -1 : 1)
            {
                for (var y = undoDirection.y > 0 ? yMax - 1 : 0; y >= 0 && y < yMax; y += undoDirection.y > 0 ? -1 : 1)
                {
                    var block = Blocks[x, y];
                    if (block != null)
                    {
                        ProcessBlock(oldModels, block, x, y);
                    }
                }
            }
        }

        private void ProcessBlock(BlockModel[,] oldModels, Block block, int x, int y)
        {
            if (BlockWasPresentLastTurn(block))
            {
                DeleteBlock(block, x, y);
            }
            else if (BlockWasMerged(block))
            {
                UnmergeBlock(oldModels, block, x, y);
            }
            else
            {
                UndoMove(oldModels, block, x, y);
            }
        }

        private void UnmergeBlock(BlockModel[,] oldModels, Block block, int x, int y)
        {
            var prevPosition1 = block.Model.PreviousPosition1;
            var prevPosition2 = block.Model.PreviousPosition2;
            var previousModel1 = oldModels[prevPosition1.x, prevPosition1.y].Clone();
            var previousModel2 = oldModels[prevPosition2.x, prevPosition2.y].Clone();
            var secondaryBlockSpawnModel = new BlockModel(block.Model.Value / 2, block.Model.Position);

            Blocks[prevPosition1.x, prevPosition1.y] = block;
            block.UndoMerge(previousModel1);

            _spawnService.SpawnBlock(secondaryBlockSpawnModel);
            Blocks[x, y].UndoMove(previousModel2);

            if (prevPosition2.x != x || prevPosition2.y != y)
            {
                Blocks[prevPosition2.x, prevPosition2.y] = Blocks[x, y];
                Blocks[x, y] = null;
            }
        }

        private void UndoMove(BlockModel[,] oldModels, Block block, int x, int y)
        {
            var prevPosition = block.Model.PreviousPosition1;
            var prevModel = oldModels[prevPosition.x, prevPosition.y].Clone();

            if (prevPosition.x != x || prevPosition.y != y)
            {
                Blocks[prevPosition.x, prevPosition.y] = block;
                Blocks[x, y] = null;
            }

            block.UndoMove(prevModel);
        }

        private static bool BlockWasMerged(Block block)
        {
            return block.Model.PreviousPosition2 != VectorUtils.EMPTY;
        }

        private void DeleteBlock(Block block, int x, int y)
        {
            Blocks[x, y] = null;
            block.Delete();
        }

        private static bool BlockWasPresentLastTurn(Block block)
        {
            return block.Model.PreviousPosition1 == VectorUtils.EMPTY;
        }
    }
}