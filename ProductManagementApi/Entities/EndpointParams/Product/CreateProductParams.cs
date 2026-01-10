using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagementApi.Entities.EndpointParams.Product
{
    public class CreateProductParams
    { 
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = false;
        [SwaggerIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
