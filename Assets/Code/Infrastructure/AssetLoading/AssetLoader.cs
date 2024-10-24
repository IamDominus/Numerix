using UnityEngine;

namespace Code.Infrastructure.AssetLoading
{
    public class AssetLoader : IAssetLoader
    {
        public T LoadAsset<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public T[] LoadAllAsset<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }

        public void UnloadAsset<T>(T asset) where T : Object
        {
            Resources.UnloadAsset(asset);
        }
    }
}