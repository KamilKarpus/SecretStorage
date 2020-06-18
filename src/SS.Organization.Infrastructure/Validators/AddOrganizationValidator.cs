using FluentValidation;
using SS.Organizations.Application.Configuration;

namespace SS.Organizations.Infrastructure.Validators
{
    public class AddOrganizationValidator : AbstractValidator<ApiCommands.V1.AddOrganization>
    {
        public AddOrganizationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        }
    }
}
