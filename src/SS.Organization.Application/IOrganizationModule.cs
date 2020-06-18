using SS.Organizations.Application.Commands;
using SS.Organizations.Application.Configuration.Queries;
using System.Threading.Tasks;

namespace SS.Organizations.Application
{
    public interface IOrganizationModule
    {
        Task ExecuteCommand(ICommand command);
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
        Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query);
    }
}
