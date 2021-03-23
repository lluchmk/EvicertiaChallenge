using FluentValidation;

namespace CalculatorService.Server.Application.Calculator.Mult
{
    public class MultRequestValidator : AbstractValidator<MultRequest>
    {
        public MultRequestValidator()
        {
            RuleFor(r => r.Factors).NotEmpty();
        }
    }
}
