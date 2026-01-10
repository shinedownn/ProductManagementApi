using ProductManagementApi.Entities.EndpointParams.Product;
using FluentValidation;

namespace ProductManagementApi.Entities.Validations
{
    public class CreateProductParamsValidator : AbstractValidator<CreateProductParams>
    {
        public CreateProductParamsValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x=>x.Category).NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x=>x.IsActive).NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x=>x.Price).NotEmpty().WithMessage("Boş geçilemez");
        }
    }
}
