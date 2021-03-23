using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;

namespace CalculatorService.Server.Application.Calculator.Sub
{
    public class SubRequest : IOperationRequest, IRequest<SubResponse>
    {
        public int Minuend { get; set; }
        public int Subtrahend { get; set; }

        public string GetOperationName()
        {
            return "sub";
        }

        public string GetFormatedRequest()
        {
            return $"{Minuend} - {Subtrahend}";
        }
    }
}
