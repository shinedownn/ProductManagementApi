using ProductManagementApi.Entities.EndpointParams.Product;
using FluentValidation;

namespace ProductManagementApi.Entities.Validations
{
    public class UpdateProductParamsValidator : AbstractValidator<UpdateProductParams>
    {
        public UpdateProductParamsValidator()
        {
            RuleFor(x => x.Id).NotEmpty().When(x => x.Id < 1).WithMessage("1'den küçük olamaz");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Boş geçilemez");
        }
    }
}
