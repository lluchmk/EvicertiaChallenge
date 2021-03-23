using CalculatorService.Server.Application.Journal.Interfaces;

namespace CalculatorService.Server.Application.Calculator.Add
{
    public class AddResponse : IOperationResponse
    {
        public int Sum { get; set; }

        public string GetFormatedResponse()
        {
            return $"{Sum}";
        }
    }
}
