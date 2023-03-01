using Newtonsoft.Json;

namespace ContextStudier.Presentation.Core.Extensions
{
    public static class HttpExtensions
    {
        private static T? Deserialize<T>(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        public static async Task<T?> GetFromJsonAsync<T>(this HttpClient client, 
            string url, JsonSerializerSettings settings)
        {
            var json = await client.GetStringAsync(url);
            
            if(json is null)
            {
                return default;
            }

            return Deserialize<T>(json, settings);
        }

        public static async Task<T?> ReadFromJsonAsync<T>(this HttpContent content, 
            JsonSerializerSettings settings)
        {
            var json = await content.ReadAsStringAsync();

            if (json is null)
            {
                return default;
            }

            return Deserialize<T>(json, settings);
        }
    }
}
