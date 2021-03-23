using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorService.Server.Application.Calculator.Sub
{
    public class SubHandler : IRequestHandler<SubRequest, SubResponse>
    {
        public Task<SubResponse> Handle(SubRequest request, CancellationToken cancellationToken)
        {
            var result = checked(request.Minuend - request.Subtrahend);
            var response = new SubResponse
            {
                Difference = result
            };

            return Task.FromResult(response);
        }
    }
}
