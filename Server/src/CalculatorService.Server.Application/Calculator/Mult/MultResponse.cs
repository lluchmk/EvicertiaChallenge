using CalculatorService.Server.Application.Journal.Interfaces;

namespace CalculatorService.Server.Application.Calculator.Mult
{
    public class MultResponse : IOperationResponse
    {
        public int Product { get; set; }

        public string GetFormatedResponse()
        {
            return $"{Product}";
        }
    }
}
