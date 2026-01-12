using ProductManagementApi.Entities.EndpointParams.Product;
using FluentValidation;

namespace ProductManagementApi.Entities.Validations
{
    public class CreateProductParamsValidator : AbstractValidator<CreateProductParams>
    {
        public CreateProductParamsValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş geçilemez").MaximumLength(500).WithMessage("İsim alanı en fazla 500 karakter olabilir."); 
            RuleFor(x=>x.Category).NotEmpty().WithMessage("Boş geçilemez").MaximumLength(50).WithMessage("Kategori alanı en fazla 500 karakter olabilir."); ;
            RuleFor(x=>x.IsActive).NotNull().WithMessage("Boş geçilemez");
            RuleFor(x=>x.Price).NotEmpty().WithMessage("Boş geçilemez").GreaterThan(0).WithMessage("Fiyat değeri 0'dan büyük olmalıdır");
        }
    }
}
