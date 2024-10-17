using Code.Gameplay;
using Code.Gameplay.Views;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public interface IGameFactory
    {
        void CreateCell(Vector3 position, Transform parent, Vector2 size);
        BlockView CreateBlockView(Vector2 position, Transform parent, Vector2 size, double value);
        Block CreateBlock(BlockModel blockModel, Vector2 position, Transform parent, Vector2 size, double value);
    }
}