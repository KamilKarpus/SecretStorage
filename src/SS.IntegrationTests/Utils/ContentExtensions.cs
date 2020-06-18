using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SS.IntegrationTests.Utils
{
    public static class ContentExtensions
    {
        public static async Task<T> CastTo<T>(this HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
}
