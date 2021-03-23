using CalculatorService.Server.Application.Journal.Interfaces;

namespace CalculatorService.Server.Application.Calculator.Div
{
    public class DivResponse : IOperationResponse
    {
        public int Quotient { get; set; }
        public int Remainder { get; set; }

        public string GetFormatedResponse()
        {
            if (Remainder == 0)
            {
                return $"{Quotient}";
            }
            else
            {
                return $"{Quotient} with remainder {Remainder}";
            }
        }
    }
}
