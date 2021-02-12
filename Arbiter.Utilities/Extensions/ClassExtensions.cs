using System.Text.Json;

namespace Arbiter.Utilities.Extensions
{
    public static class ClassExtensions
    {
        public static T DeepClone<T>(this T obj)
        {
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj));
        }
    }
}
