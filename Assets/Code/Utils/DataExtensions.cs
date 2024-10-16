using Code.Data;
using UnityEngine;

namespace Code.Utils
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToSerialized(this GameSaveData data)
        {
            return JsonUtility.ToJson(data);
        }
    }
}