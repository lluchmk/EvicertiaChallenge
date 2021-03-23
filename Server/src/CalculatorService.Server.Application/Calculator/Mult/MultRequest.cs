using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace CalculatorService.Server.Application.Calculator.Mult
{
    public class MultRequest : IOperationRequest, IRequest<MultResponse>
    {
        public IEnumerable<int> Factors { get; set; }

        public string GetOperationName()
        {
            return "mult";
        }

        public string GetFormatedRequest()
        {
            return string.Join(" * ", Factors);
        }
    }
}
