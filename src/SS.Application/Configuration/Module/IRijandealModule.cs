using SS.Application.Configuration.Proccesing;
using System.Threading.Tasks;

namespace SS.Application.Configuration.Module
{
    public interface IRijandealModule
    {
        Task ExecuteCommand(ICommand command);
        //Task<TResult> ExecuteQuery<TResult>(IQuery<TResult> query);
    }
}
