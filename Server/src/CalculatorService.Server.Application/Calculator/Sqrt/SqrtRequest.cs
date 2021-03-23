using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;

namespace CalculatorService.Server.Application.Calculator.Sqrt
{
    public class SqrtRequest : IOperationRequest, IRequest<SqrtResponse>
    {
        public int Number { get; set; }

        public string GetOperationName()
        {
            return "sqrt";
        }

        public string GetFormatedRequest()
        {
            return $"{(char)0x221A}{Number}";
        }
    }
}
