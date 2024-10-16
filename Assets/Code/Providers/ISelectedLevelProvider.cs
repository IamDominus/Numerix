using Code.Infrastructure;
using UnityEngine;

namespace Code.Providers
{
    public interface ISelectedLevelProvider
    {
        Observable<Vector2Int> Level { get; set; }
    }
}