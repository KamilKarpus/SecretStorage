using FluentValidation;
using SS.Organizations.Application.Configuration;

namespace SS.Organizations.Infrastructure.Validators
{
    public class AddUserToOrganizationValidator : AbstractValidator<ApiCommands.V1.AddUserToOrganization>
    {
        public AddUserToOrganizationValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}
