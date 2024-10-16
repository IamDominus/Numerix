using Code.Data;
using Newtonsoft.Json;

namespace Code.Utils
{
    public static class SerializationExtensions
    {
        public static T ToDeserialized<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToSerialized(this GameSaveData data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }
}