using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation;

namespace CleanArchitecture.WebApi.Filters
{
    public class ValidationFilter: IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null)
                {
                    continue;
                }
                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType);
                if (validator == null)
                {
                    continue;
                }

                var validationContext = new ValidationContext<object>(argument);
                var result = await ((IValidator)validator).ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                   throw new ValidationException(result.Errors);
                }
            }
            await next();
        }
    }
}
