using Code.Logic;
using UnityEngine;

namespace Code.Infrastructure.Factories
{
    public interface IGameFactory
    {
        void CreateCell(Vector3 position, Transform parent, Vector2 size);
        BlockView CreateBlock(Vector2 position, Transform parent, Vector2 size, long value);
    }
}