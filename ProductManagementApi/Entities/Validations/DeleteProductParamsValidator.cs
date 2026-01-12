using ProductManagementApi.Entities.EndpointParams.Product;
using FluentValidation;

namespace ProductManagementApi.Entities.Validations
{
    public class DeleteProductParamsValidator : AbstractValidator<DeleteProductParams>
    {
        public DeleteProductParamsValidator()
        {
            RuleFor(x => x.Id).NotEmpty().LessThan(1).WithMessage("Id değeri 1'den küçük olamaz");
        }
    }
}
