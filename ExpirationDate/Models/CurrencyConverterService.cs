using Newtonsoft.Json.Linq;

namespace ExpirationDate.Models
{
    public class CurrencyService 
    {
        private readonly string _apiKey = "ca2ab85d96bc49c131a9125b";
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync()
        {
            var result = new Dictionary<string, decimal>();
            string urlUsd = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/USD/UAH";
            string urlEur = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/EUR/UAH";

            HttpResponseMessage responseUsd = await _httpClient.GetAsync(urlUsd);
            responseUsd.EnsureSuccessStatusCode();
            string jsonResponseUsd = await responseUsd.Content.ReadAsStringAsync();
            JObject jsonUsd = JObject.Parse(jsonResponseUsd);
            result["USD"] = (decimal)jsonUsd["conversion_rate"];

            HttpResponseMessage responseEur = await _httpClient.GetAsync(urlEur);
            responseEur.EnsureSuccessStatusCode();
            string jsonResponseEur = await responseEur.Content.ReadAsStringAsync();
            JObject jsonEur = JObject.Parse(jsonResponseEur);
            result["EUR"] = (decimal)jsonEur["conversion_rate"];

            return result;
        }
    }
}
