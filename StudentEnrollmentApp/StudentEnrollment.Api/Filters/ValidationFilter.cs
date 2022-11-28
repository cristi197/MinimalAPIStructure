using StudentEnrollment.Api.DTOs.Authentication;
using StudentEnrollment.Api.DTOs;
using FluentValidation;

namespace StudentEnrollment.Api.Filters
{
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator;
        public ValidationFilter(IValidator<T> validator)
        {
            this._validator = validator;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //runs before endpoint code
            var objectToValidate = context.GetArgument<T>(0);

            var validationResult = await _validator.ValidateAsync(objectToValidate);

            var errors = new List<ErrorResponseDto>();
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            var result = await next(context);

            //do something after endpoint code

            return result;
        }
    }
}
