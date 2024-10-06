using Code.Services;
using Code.Utils;
using Code.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Logic
{
    public class Block
    {
        public BlockModel Model { get; private set; }
        private BlockView View { get; set; }
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

        public void Move(Vector2Int position)
        {
            Model.PreviousPosition1 = Model.Position;
            Model.PreviousPosition2 = VectorUtils.EMPTY;
            Model.Position = position;
            Model.MovedThisTurn = true;

            View.Move(position);
        }

        public void Merge(Block targetBlock)
        {
            Model.PreviousPosition1 = Model.Position;
            Model.PreviousPosition2 = targetBlock.Model.PreviousPosition1; //PreviousPosition1 since Move executed already for targetBlock
            Model.Position = targetBlock.Model.Position;
            Model.Value *= 2;
            Model.MergedThisTurn = true;
            Model.MovedThisTurn = true;

            View.Move(Model.Position);
            RespawnViewWithDelay().Forget();
        }

        public void UndoMove(BlockModel oldModel)
        {
            View.Move(oldModel.Position);
            Model = oldModel;
        }

        public void UndoMerge(BlockModel oldModel)
        {
            View.Delete();
            Model.Value /= 2;
            RespawnView();
            View.Move(oldModel.Position);
            Model = oldModel;
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

        private void RespawnView()
        {
            View = _spawnService.SpawnBlockView(Model);
        }

        private async UniTask RespawnViewWithDelay()
        {
            await View.DeleteWithDelay();
            RespawnView();
        }

        public void Delete()
        {
            View.Delete();
        }

        public void DeleteWithDelay()
        {
            View.DeleteWithDelay().Forget();
        }
    }
}