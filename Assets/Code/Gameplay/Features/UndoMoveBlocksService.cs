using Code.Services;
using Code.Utils;
using Cysharp.Threading.Tasks;

namespace Code.Gameplay.Features
{
    public class UndoMoveBlocksService : IUndoMoveBlocksService
    {
        private Block[,] Blocks => _levelDataRepository.Blocks;

        private readonly ISpawnService _spawnService;
        private readonly IInputService _inputService;
        private readonly ILevelDataRepository _levelDataRepository;

        public UndoMoveBlocksService(ISpawnService spawnService, IInputService inputService, ILevelDataRepository levelDataRepository)
        {
            _spawnService = spawnService;
            _inputService = inputService;
            _levelDataRepository = levelDataRepository;
        }

        public async UniTask UndoTurn()
        {
            // TODO move out async logic 
            if (_levelDataRepository.TurnHistoryCount() <= 0)
                return;
            _inputService.Disable();

            var oldModels = _levelDataRepository.PopPreviousTurnBlockModels();
            var undoDirection = _levelDataRepository.PopPreviousTurnMoveDirection() * -1;

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

            await UniTask.WaitForSeconds(Constants.MOVE_ANIMATION_TIME_SEC);
            _inputService.Enable();
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