using FluentValidation;
using SS.Users.Application.Configuration;
using SS.Users.Infrastructure.Validators.CustomValidators;

namespace SS.Users.Infrastructure.Validators
{
    public class UserRegisterValidator : AbstractValidator<ApiCommands.V1.RegisterUser>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Password).Must(ValidatePassword).WithMessage(ErrorMessage.PasswordError).NotEmpty();
            RuleFor(x => x.DisplayName).NotEmpty().MaximumLength(100);
        }
        private bool ValidatePassword(string password)
            => new PasswordValidator().ValidatePassword(password);
    }
}
