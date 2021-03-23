using FluentValidation;

namespace CalculatorService.Server.Application.Calculator.Sqrt
{
    public class SqrtRequestValidator : AbstractValidator<SqrtRequest>
    {
        public SqrtRequestValidator()
        {
            RuleFor(r => r.Number).GreaterThan(-1);
        }
    }
}
