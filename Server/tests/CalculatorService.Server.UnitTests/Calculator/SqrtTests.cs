using CalculatorService.Server.Application.Calculator.Sqrt;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Calculator
{
    public class SqrtTests
    {
        [Fact]
        public async Task SqrtHandler_CalculatesSquareRoot()
        {
            var request = new SqrtRequest
            {
                Number = TestHelper.GeneratePositiveNumber()
            };
            var expected = Math.Sqrt(request.Number);
            var handler = new SqrtHandler();

            var response = await handler.Handle(request, default);

            response.Square.Should().Be(expected);
        }

        [Fact]
        public void SqrtRequest_CorrectlyFormatsRequestForJournal()
        {
            var number = TestHelper.GeneratePositiveNumber();
            var expected = $"{(char)0x221A}{number}";
            var request = new SqrtRequest
            {
                Number = number
            };

            var formattedRequest = request.GetFormatedRequest();

            formattedRequest.Should().Be(expected);
        }

        [Fact]
        public void SqrtRequest_CorrectlyReturnsOperationname()
        {
            var expected = "sqrt";
            var request = new SqrtRequest();

            var operationName = request.GetOperationName();

            operationName.Should().Be(expected);
        }

        [Fact]
        public void SqrtResponse_CorrectlyFormatsResponseForJournal()
        {
            var sqrt = TestHelper.GeneratePositiveNumber();
            var expected = sqrt.ToString();
            var response = new SqrtResponse
            {
                Square = sqrt
            };

            var formattedResponse = response.GetFormatedResponse();

            formattedResponse.Should().Be(expected);
        }

        [Fact]
        public void SqrtRequestValidator_WithNumberDifferentTo0_IsSuccessful()
        {
            var request = new SqrtRequest
            {
                Number = 5
            };
            var validator = new SqrtRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void SqrtRequestValidator_WithNNumberLowerThan0_IsNotSuccessful()
        {
            var request = new SqrtRequest
            {
                Number = -1
            };
            var validator = new SqrtRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }
    }
}
