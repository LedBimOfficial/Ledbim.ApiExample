using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ledbim.ApiExample.Application.PipelineBehaviors
{
    internal class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger = null!;

        public PerformanceBehavior(ILogger<TRequest> logger)
            => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var timer = Stopwatch.StartNew();

            var response = await next();

            timer.Stop();

            var elapsed = timer.ElapsedMilliseconds;
            if (elapsed <= 1000)
                return response;

            var requestName = typeof(TRequest).Name;

            _logger.LogWarning("Long Running Request: {Name} ({Elapsed} ms) {Request}", requestName, elapsed, request);

            return response;
        }
    }
}