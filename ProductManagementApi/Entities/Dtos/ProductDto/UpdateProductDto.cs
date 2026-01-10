namespace ProductManagementApi.Entities.Dtos.ProductDto
{
    public class UpdateProductDto : IDto
    { 
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } 
    }
}
