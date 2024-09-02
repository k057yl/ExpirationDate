using Newtonsoft.Json.Linq;

namespace ExpirationDate.Models
{
    public class CurrencyConverterService
    {
        private readonly string _apiKey = "ca2ab85d96bc49c131a9125b";
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CurrencyConverterService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            string url = $"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{fromCurrency}/{toCurrency}";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonResponse);

            return (decimal)jsonObject["conversion_rate"];
        }

        public async Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
        {
            decimal rate = await GetExchangeRateAsync(fromCurrency, toCurrency);
            return amount * rate;
        }

        public async Task<decimal> ConvertPriceForLanguageAsync(decimal amount, string languageCode)
        {
            var defaultCurrency = "UAH";
            var targetCurrency = _configuration[$"CurrencySettings:{languageCode}"] ?? defaultCurrency;

            return await ConvertAsync(amount, defaultCurrency, targetCurrency);
        }
    }
}
