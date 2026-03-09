using SunatIntegration.Application.DTOs.Sunat;
using SunatIntegration.Application.Interfaces;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace SunatIntegration.Infrastructure.ExternalServices.Sunat
{
    public class SunatApiClient : ISunatApiClient
    {
        private readonly HttpClient _httpClient;

        public SunatApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");
        }
        public async Task<ExchangeRateDto> GetExchangeRateAsync()
        { 
            var payload = new
            {
                anio = DateTime.Now.Year,
                mes = DateTime.Now.Month - 1,
                token = Guid.NewGuid().ToString("N")
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var _url = "https://e-consulta.sunat.gob.pe/cl-at-ittipcam/tcS01Alias/listarTipoCambio";

            var response = await _httpClient.PostAsync(_url, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var tipoCambios = JsonSerializer.Deserialize<List<SunatTypeChangeDto>>(result);

            var today = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var typeChangeToday = tipoCambios.Where(x => x.fecPublica == today);

            double? priceSales = null;
            double? pricePurchase = null;
            string fecPublica = today;

            if (typeChangeToday.Any())
            {
                foreach (var tipoCambio in typeChangeToday)
                {
                    fecPublica = tipoCambio.fecPublica;
                    if (tipoCambio.codTipo == "V")
                        priceSales = double.Parse(tipoCambio.valTipo, CultureInfo.InvariantCulture);
                    if (tipoCambio.codTipo == "C")
                        pricePurchase = double.Parse(tipoCambio.valTipo, CultureInfo.InvariantCulture);
                }
            }
          
            return new ExchangeRateDto()
            {
                DatePublic =DateTime.Parse(fecPublica),
                Pricepurchase = pricePurchase,
                PriceSales = priceSales

            };

        }
    }
}
