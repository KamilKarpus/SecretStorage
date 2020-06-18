using MediatR;
using Serilog;
using SS.Users.Application.Configuration.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.Configuration.Proccesing
{
    public class LoggingCommnadHandlerDecarator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<T> _decorated;
        public LoggingCommnadHandlerDecarator(ICommandHandler<T> decorated, ILogger logger)
        {
            _logger = logger;
            _decorated = decorated;
        }
        public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                var result  = await _decorated.Handle(request, cancellationToken);
                _logger.Information($"Handled Commnad {request.GetType()} sucessfull");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Command {request.GetType()} proccesing failed. Exception {ex.Message}");
                throw;

            }
        }
    }
}
