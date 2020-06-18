using MediatR;
using Serilog;
using SS.Organizations.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.Configuration.Proccesing
{
    public class LoggingCommnadHandlerDecarator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ILogger _logger;
        private readonly ICommandHandler<T> _decorated;
        public LoggingCommnadHandlerDecarator(ILogger logger, ICommandHandler<T> decorated)
        {
            _logger = logger;
            _decorated = decorated;
        }
        public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
        {
            try
            {
                await _decorated.Handle(request, cancellationToken);
                _logger.Information($"Handled Commnad {request.GetType()} sucessfull");
            }
            catch (Exception ex)
            {
                _logger.Error($"Command {request.GetType()} proccesing failed. Exception {ex.Message}");
                throw;

            }
            return Unit.Value;
        }
    }
}
