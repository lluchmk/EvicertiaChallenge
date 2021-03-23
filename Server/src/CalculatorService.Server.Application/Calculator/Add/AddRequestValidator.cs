using FluentValidation;

namespace CalculatorService.Server.Application.Calculator.Add
{
    public class AddRequestValidator : AbstractValidator<AddRequest>
    {
        public AddRequestValidator()
        {
            RuleFor(r => r.Addends).NotEmpty();
        }
    }
}
