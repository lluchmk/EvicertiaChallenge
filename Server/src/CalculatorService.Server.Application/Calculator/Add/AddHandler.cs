using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CalculatorService.Server.Application.Calculator.Add
{
    public class AddHandler : IRequestHandler<AddRequest, AddResponse>
    {
        public Task<AddResponse> Handle(AddRequest request, CancellationToken cancellationToken)
        {
            var result = checked(request.Addends.Aggregate((acc, x) => acc + x));
            var response = new AddResponse
            {
                Sum = result
            };

            return Task.FromResult(response);
        }
    }
}
