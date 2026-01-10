using ProductManagementApi.Entities.EndpointParams.Product;
using FluentValidation;

namespace ProductManagementApi.Entities.Validations
{
    public class DeleteProductParamsValidator : AbstractValidator<DeleteProductParams>
    {
        public DeleteProductParamsValidator()
        {
            RuleFor(x => x.Id).NotEmpty().When(x => x.Id < 1).WithMessage("1'den küçük olamaz");
        }
    }
}
