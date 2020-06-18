using SS.Users.Application.Configuration.Commands;
using SS.Users.Application.Configuration.Queries;
using System.Threading.Tasks;

namespace SS.Users.Application
{
    public interface IUserModule
    {
        Task ExecuteCommand(ICommand command);
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
        Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query);
    }
}
