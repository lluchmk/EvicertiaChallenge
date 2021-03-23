using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorService.Server.Application.Calculator.Div
{
    public class DivHandler : IRequestHandler<DivRequest, DivResponse>
    {
        public Task<DivResponse> Handle(DivRequest request, CancellationToken cancellationToken)
        {
            var quotient = Math.DivRem(request.Dividend, request.Divisor, out int remainder);
            var response = new DivResponse
            {
                Quotient = quotient,
                Remainder = remainder
            };

            return Task.FromResult(response);
        }
    }
}
