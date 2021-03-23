using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;

namespace CalculatorService.Server.Application.Calculator.Div
{
    public class DivRequest : IOperationRequest, IRequest<DivResponse>
    {
        public int Dividend { get; set; }
        public int Divisor { get; set; }

        public string GetOperationName()
        {
            return "div";
        }

        public string GetFormatedRequest()
        {
            return $"{Dividend} / {Divisor}";
        }
    }
}
