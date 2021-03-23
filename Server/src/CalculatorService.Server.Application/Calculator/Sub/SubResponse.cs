using CalculatorService.Server.Application.Journal.Interfaces;

namespace CalculatorService.Server.Application.Calculator.Sub
{
    public class SubResponse : IOperationResponse
    {
        public decimal Difference { get; set; }

        public string GetFormatedResponse()
        {
            return $"{Difference}";
        }
    }
}
