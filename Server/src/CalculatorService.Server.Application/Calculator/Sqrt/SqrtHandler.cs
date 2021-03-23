using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorService.Server.Application.Calculator.Sqrt
{
    public class SqrtHandler : IRequestHandler<SqrtRequest, SqrtResponse>
    {
        public Task<SqrtResponse> Handle(SqrtRequest request, CancellationToken cancellationToken)
        {
            var result = Math.Sqrt(request.Number);
            var response = new SqrtResponse
            {
                Square = result
            };

            return Task.FromResult(response);
        }
    }
}
