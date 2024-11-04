using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace Ledbim.ApiExample.Application.PipelineBehaviors
{
    internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger = null!;

        public LoggingBehavior(ILogger<TRequest> logger)
            => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = request.GetType().Name;
            var requestGuid = Guid.NewGuid().ToString();
            var requestNameWithGuid = $"{requestName} [{requestGuid}]";

            _logger.LogInformation("[STARTED] {Id}", requestNameWithGuid);

            try
            {
                _logger.LogInformation("[REQUEST] {Id} {Request}", requestNameWithGuid, JsonSerializer.Serialize(request));
            }
            catch (NotSupportedException)
            {
                _logger.LogInformation("[SERIALIZATION ERROR] {Id} Could not serialize the request.", requestNameWithGuid);
            }

            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                response = await next();

                _logger.LogInformation("[RESPONSE] {Id} {Response}", requestNameWithGuid, JsonSerializer.Serialize(response));

            }
            finally
            {
                _logger.LogInformation("[END] {Id}; Execution time={Time}ms", requestNameWithGuid, stopwatch.ElapsedMilliseconds);
            }
            stopwatch.Stop();

            return response;
        }
    }
}