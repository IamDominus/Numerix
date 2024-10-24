using UnityEngine;

namespace Code.Infrastructure.AssetLoading
{
    public interface IAssetLoader
    {
        T LoadAsset<T>(string path) where T : Object;
        T[] LoadAllAsset<T>(string path) where T : Object;
        void UnloadAsset<T>(T asset) where T : Object;
    }
}