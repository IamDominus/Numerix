using Code.Services;
using Code.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic
{
    public class Block
    {
        public BlockModel Model { get; private set; }
        public BlockView View { get; private set; }

        private readonly ISpawnService _spawnService;

        public Block(ISpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public void Initialize(BlockModel model, BlockView view)
        {
            Model = model;
            View = view;
        }

        public void Move(Vector2Int position, bool deleteAfterMove)
        {
            Model.PreviousPosition1 = Model.Position;
            Model.PreviousPosition2 = VectorUtils.EMPTY;
            Model.Position = position;
            Model.MergedThisTurn = deleteAfterMove;
            Model.MovedThisTurn = true;

            View.Move(position, deleteAfterMove);
        }

        public void MergeWithBlock(Block block)
        {
            if (Model.MovedThisTurn == false)
            {
                Model.PreviousPosition1 = Model.Position;
            }

            Model.Value *= 2;
            Model.PreviousPosition2 = block.Model.Position;
            Model.MergedThisTurn = true;

            RespawnView().Forget();
        }

        public bool CanMergeWith(Block block)
        {
            return block.Model.Value == Model.Value && block.Model.MergedThisTurn == false && Model.MergedThisTurn == false;
        }

        public void ResetFlags()
        {
            Model.MergedThisTurn = false;
            Model.MovedThisTurn = false;
        }

        private async UniTask RespawnView()
        {
            await View.Delete();
            View = _spawnService.SpawnBlockView(Model);
        }
    }
}