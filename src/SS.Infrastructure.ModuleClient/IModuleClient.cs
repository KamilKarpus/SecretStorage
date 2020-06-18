using MediatR;
using System.Threading.Tasks;

namespace SS.Infrastructure.ModuleClient
{
    public interface IModuleClient
    {
        Task<TResult> GetAsync<TResult>(string path, string restofpath, object request) where TResult : class;
        void AddEndpointDefination<parameter, result>(string path, string method);
        void AddEndpointDispatcher(string path, IMediator mediator);
    }
}