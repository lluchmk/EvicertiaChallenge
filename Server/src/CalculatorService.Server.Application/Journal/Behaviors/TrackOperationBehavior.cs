using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorService.Server.Application.Journal.Behaviors
{
    public class TrackOperationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IOperationRequest
        where TResponse : IOperationResponse
    {

        private readonly IJournalService _journalService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TrackOperationBehavior(IJournalService journalService, IHttpContextAccessor httpContextAccessor)
        {
            _journalService = journalService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            var trackId = _httpContextAccessor.HttpContext.Request.Headers["X-Evi-Tracking-Id"];
            if (!string.IsNullOrWhiteSpace(trackId))
            {
                _journalService.RegisterOperation(trackId, request, response, DateTime.UtcNow);
            }

            return response;
        }
    }
}
