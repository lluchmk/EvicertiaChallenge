using CalculatorService.Server.Application.Journal.Interfaces;

namespace CalculatorService.Server.Application.Calculator.Sqrt
{
    public class SqrtResponse : IOperationResponse
    {
        public double Square { get; set; }

        public string GetFormatedResponse()
        {
            return $"{Square}";
        }
    }
}
