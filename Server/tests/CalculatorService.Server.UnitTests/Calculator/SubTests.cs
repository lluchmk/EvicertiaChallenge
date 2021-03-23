using CalculatorService.Server.Application.Calculator.Sub;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Calculator
{
    public class SubTests
    {
        [Fact]
        public async Task SubHandler_SubtractsSubtrahendToMinuend()
        {
            var request = new SubRequest
            {
                Minuend = TestHelper.GeneratePositiveNumber(),
                Subtrahend = TestHelper.GeneratePositiveNumber()
            };
            var expected = request.Minuend - request.Subtrahend;
            var handler = new SubHandler();

            var response = await handler.Handle(request, default);

            response.Difference.Should().Be(expected);
        }

        [Fact]
        public void SubRequest_CorrectlyFormatsRequestForJournal()
        {
            var minuend = TestHelper.GeneratePositiveNumber();
            var subtrahend = TestHelper.GeneratePositiveNumber();
            var expected = $"{minuend} - {subtrahend}";
            var request = new SubRequest
            {
                Minuend = minuend,
                Subtrahend = subtrahend
            };

            var formattedRequest = request.GetFormatedRequest();

            formattedRequest.Should().Be(expected);
        }

        [Fact]
        public void SubRequest_CorrectlyReturnsOperationname()
        {
            var expected = "sub";
            var request = new SubRequest();

            var operationName = request.GetOperationName();

            operationName.Should().Be(expected);
        }

        [Fact]
        public void SubResponse_CorrectlyFormatsResponseForJournal()
        {
            var difference = TestHelper.GeneratePositiveNumber();
            var expected = difference.ToString();
            var response = new SubResponse
            {
                Difference = difference
            };

            var formattedResponse = response.GetFormatedResponse();

            formattedResponse.Should().Be(expected);
        }
    }
}
