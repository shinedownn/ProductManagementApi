namespace ProductManagementApi.Entities.Dtos.ProductDto
{
    public class CreateProductDto : IDto
    { 
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
