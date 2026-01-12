using ProductManagementApi.Services.FakestoreApi.Abstract;
using ProductManagementApi.Services.FakestoreApi.Models;

namespace ProductManagementApi.Services.FakestoreApi.Concrete
{
    public class ExternalProductService : IExternalProductService
    {
        private string _baseAddress = "https://fakestoreapi.com/";
        private HttpClient _client;
        public ExternalProductService()
        {
            _client = HttpClientFactory.Create();
            _client.BaseAddress = new Uri(_baseAddress);
        }

        public async Task<IEnumerable<ExternalProductModel>> GetProducts()
        {
            var products = await _client.GetFromJsonAsync<IEnumerable<ExternalProductModel>>("products");
            return products;
        }

        public async Task<IEnumerable<ExternalProductModel>> GetProductByRandomPrice()
        {
            var products = await _client.GetFromJsonAsync<List<ExternalProductModel>>("products");
            products.ForEach(
                    x => x.Price = Random.Shared.Next(1, 10) % 2 == 0
                    ? GetRandomPrice(x.Price)
                    : x.Price                     
                    );
            return products;
        }
        /// <summary>
        /// Api den gelen fiyatın yarısı ile iki katı arasında rastgele bir fiyat döner
        /// </summary>
        /// <param name="basePrice"></param>
        /// <returns>Yeni fiyat döner</returns>
        private static decimal GetRandomPrice(decimal basePrice)
        {
            double min = (double)basePrice / 2;
            double max = (double)basePrice * 2;
            decimal finalPrice;
            double randomDouble;
            if (Random.Shared.Next(3) % 2 == 0)
            {
                randomDouble = Random.Shared.NextDouble() * (max - min) + Random.Shared.Next(Convert.ToInt32(min),Convert.ToInt32(max));
                finalPrice = Math.Round((decimal)randomDouble, 2);
            }
            else
            {
                randomDouble = Random.Shared.NextDouble() * (max - min) - Random.Shared.Next(Convert.ToInt32(min), Convert.ToInt32(max));
                finalPrice = Math.Round((decimal)randomDouble, 2);
            }
            return finalPrice<0?-finalPrice:finalPrice; 
        }
    }    
}
