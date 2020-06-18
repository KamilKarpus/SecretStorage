using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Application.Configuration.Queries;
using System;
using System.Threading.Tasks;

namespace SS.Collections.Application
{
    public interface ICollectionsModule
    {
        Task ExecuteCommand(ICommand command);
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
        Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query);
    }
}
