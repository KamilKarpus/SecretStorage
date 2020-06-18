using FluentValidation;
using SS.Organizations.Application.Configuration;

namespace SS.Organizations.Infrastructure.Validators
{
    public class ChangeUserRoleValidator : AbstractValidator<ApiCommands.V1.ChangeUserRole> 
    {
        public ChangeUserRoleValidator()
        {
            RuleFor(p => p.RoleId).NotEmpty().InclusiveBetween(1, 3);
        }
    }
}
