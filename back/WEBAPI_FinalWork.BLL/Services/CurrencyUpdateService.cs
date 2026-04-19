using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace WEBAPI_FinalWork.BLL.Services
{
    public class CurrencyUpdateService
    {
        private readonly HttpClient _httpClient;
        private static float _currentRate = 41.5f;

        public CurrencyUpdateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task UpdateUAHAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MonoCurrencyResponse>>("https://api.monobank.ua/bank/currency");
                var usdRate = response?.FirstOrDefault(x => x.CurrencyCodeA == 840 && x.CurrencyCodeB == 980);

                if (usdRate != null)
                {
                    _currentRate = usdRate.RateSell;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public float GetCurrentRate()
        {
            return _currentRate;
        }
        public class MonoCurrencyResponse
        {
            public int CurrencyCodeA { get; set; }
            public int CurrencyCodeB { get; set; }
            public float RateSell { get; set; }
        }
    }
}
