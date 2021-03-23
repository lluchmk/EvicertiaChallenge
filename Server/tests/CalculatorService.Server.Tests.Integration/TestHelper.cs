using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculatorService.Server.Tests.Integration
{
    public static class TestHelper
    {
        public static async Task<HttpResponseMessage> PostJsonAsync<TRequest>(this HttpClient client, string uri, TRequest request)
        {
            var serializedRequest = JsonSerializer.Serialize(request);
            var jsonRequest = new StringContent(serializedRequest, Encoding.UTF8, "application/json");

            return await client.PostAsync(uri, jsonRequest);
        }

        public static HttpClient WithTrackIdHeader(this HttpClient client, string trackId)
        {
            client.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", trackId);
            return client;
        }

        public static async Task<TResponse> GetResponse<TResponse>(this HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseContent);
        }
    }
}
