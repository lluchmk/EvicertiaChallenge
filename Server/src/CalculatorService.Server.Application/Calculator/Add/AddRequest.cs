using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace CalculatorService.Server.Application.Calculator.Add
{
    public class AddRequest : IOperationRequest, IRequest<AddResponse>
    {
        public IEnumerable<int> Addends { get; set; }

        public string GetOperationName()
        {
            return "sum";
        }

        public string GetFormatedRequest()
        {
            return string.Join(" + ", Addends);
        }
    }
}
