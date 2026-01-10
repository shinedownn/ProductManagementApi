using ProductManagementApi.Entities.Dtos.ProductDto;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;

namespace ProductManagementApi.Services
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

        public async Task<IEnumerable<ExternalProduct>> GetProducts()
        {
            var data = await _client.GetFromJsonAsync<IEnumerable<ExternalProduct>>("products");
            return data;
        }

        public async Task<IEnumerable<ExternalProduct>> GetProductByRandomPrice()
        {
            var data = await _client.GetFromJsonAsync<List<ExternalProduct>>("products");
             
            data.ForEach(
                    x => x.Price = ((Random.Shared.Next(1, 10) % 2 == 0)
                    ?
                      GetRandomPrice(x.Price)
                    : x.Price)
                     
                    );
            



            return data;
        }
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
    public class ExternalProduct
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("rating")]
        public Rating Rating { get; set; }
    }

    public class Rating
    {
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
