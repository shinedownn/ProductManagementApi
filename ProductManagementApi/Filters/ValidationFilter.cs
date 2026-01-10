using ProductManagementApi.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductManagementApi.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        { 
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue; 
                 
                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationContext = new ValidationContext<object>(argument);
                    var result = await validator.ValidateAsync(validationContext);

                    if (!result.IsValid)
                    {
                        var errors = result.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(
                                g => g.Key,
                                g => g.Select(e => e.ErrorMessage).ToArray()
                            ); 
                        ResponseModel<object> response = new ResponseModel<object>()
                        {
                            Status=false,
                            Data=null,
                            Message="Hata bulundu",
                            Errors= errors.Select((k) => k.Key +" : " + string.Join(",", k.Value)).ToList()
                        };
                        context.Result = new BadRequestObjectResult(response);

                        return;  
                    }
                }
            }

            await next();  
        }
    }
}
