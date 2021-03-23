using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorService.Server.Application.Calculator.Mult
{
    public class MultHandler : IRequestHandler<MultRequest, MultResponse>
    {
        public Task<MultResponse> Handle(MultRequest request, CancellationToken cancellationToken)
        {
            var result = checked(request.Factors.Aggregate((acc, x) => acc * x));
            var response = new MultResponse
            {
                Product = result
            };

            return Task.FromResult(response);
        }
    }
}
