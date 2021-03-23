using FluentValidation;

namespace CalculatorService.Server.Application.Calculator.Div
{
    public class DivRequestValidator : AbstractValidator<DivRequest>
    {
        public DivRequestValidator()
        {
            RuleFor(r => r.Divisor).NotEqual(0);
        }
    }
}
