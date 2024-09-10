using ExpirationDate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ExpirationDate.Views.Shared.Components.CurrencyRates
{
    public class CurrencyRatesViewComponent : ViewComponent
    {
        private readonly CurrencyService _currencyService;
        private readonly IMemoryCache _cache;

        public CurrencyRatesViewComponent(CurrencyService currencyService, IMemoryCache cache)
        {
            _currencyService = currencyService;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cacheKey = "currencyRates";
            if (!_cache.TryGetValue(cacheKey, out Dictionary<string, decimal> currencyRates))
            {
                currencyRates = await _currencyService.GetExchangeRatesAsync();

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };
                _cache.Set(cacheKey, currencyRates, cacheOptions);
            }

            return View(currencyRates);
        }
    }
}
