using eCommerceApp.Application.DTOs;
using FluentValidation;

namespace eCommerceApp.Application.Validations
{
    public class ValidationService : IValidationService
    {
        public async Task<AuthResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {
            var validationResult = await validator.ValidateAsync(model);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                string errorsToString = string.Join(", ", errors);
                return new AuthResponse { Message = errorsToString };
            }
            return new AuthResponse { Success = true };
        }
    }
}
