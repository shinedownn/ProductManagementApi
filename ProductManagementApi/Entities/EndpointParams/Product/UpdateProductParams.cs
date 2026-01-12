using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagementApi.Entities.EndpointParams.Product
{
    public class UpdateProductParams
    {
        public string Name { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; } 
    }
}
