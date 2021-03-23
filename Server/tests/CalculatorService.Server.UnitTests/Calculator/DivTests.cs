using CalculatorService.Server.Application.Calculator.Div;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Calculator
{
    public class DivTests
    {
        [Fact]
        public async Task DivHandler_DividesDividendByDivisor()
        {
            var request = new DivRequest
            {
                Dividend = TestHelper.GeneratePositiveNumber(),
                Divisor = TestHelper.GeneratePositiveNumber()
            };

            var expectedQuotiend = request.Dividend / request.Divisor;
            var expectedRemainder = request.Dividend % request.Divisor;
            var handler = new DivHandler();

            var response = await handler.Handle(request, default);

            using (new AssertionScope())
            {
                response.Quotient.Should().Be(expectedQuotiend);
                response.Remainder.Should().Be(expectedRemainder);
            }
        }

        [Fact]
        public void DivRequest_CorrectlyFormatsRequestForJournal()
        {
            var dividend = TestHelper.GeneratePositiveNumber();
            var divisor = TestHelper.GeneratePositiveNumber();
            var expected = $"{dividend} / {divisor}";
            var request = new DivRequest
            {
                Dividend = dividend,
                Divisor = divisor
            };

            var formattedRequest = request.GetFormatedRequest();

            formattedRequest.Should().Be(expected);
        }

        [Fact]
        public void DivRequest_CorrectlyReturnsOperationname()
        {
            var expected = "div";
            var request = new DivRequest();

            var operationName = request.GetOperationName();

            operationName.Should().Be(expected);
        }

        [Fact]
        public void DivResponse_CorrectlyFormatsResponseForJournal()
        {
            var quotiend = TestHelper.GeneratePositiveNumber();
            var remainder = TestHelper.GeneratePositiveNumber();
            var expected = $"{quotiend} with remainder {remainder}";
            var response = new DivResponse
            {
                Quotient = quotiend,
                Remainder = remainder
            };

            var formattedResponse = response.GetFormatedResponse();

            formattedResponse.Should().Be(expected);
        }

        [Fact]
        public void DivRequestValidator_WithDivisorDifferentTo0_IsSuccessful()
        {
            var request = new DivRequest
            {
                Dividend = TestHelper.GeneratePositiveNumber(),
                Divisor = TestHelper.GeneratePositiveNumber()
            };
            var validator = new DivRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void DivRequestValidator_WithDivisor0_IsNotSuccessful()
        {
            var request = new DivRequest
            {
                Dividend = TestHelper.GeneratePositiveNumber(),
                Divisor = 0
            };
            var validator = new DivRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }
    }
}
