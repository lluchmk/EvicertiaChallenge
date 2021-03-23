using CalculatorService.Server.Application.Calculator.Add;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Calculator
{
    public class AddTests
    {
        [Fact]
        public async Task AddHandler_SumsAddends()
        {
            var addends = TestHelper.GeneratePositiveNumbers();
            var request = new AddRequest
            {
                Addends = addends
            };
            var expected = 0;
            foreach (var addend in request.Addends)
            {
                expected += addend;
            }
            var handler = new AddHandler();

            var response = await handler.Handle(request, default);

            response.Sum.Should().Be(expected);
        }

        [Fact]
        public void AddRequest_CorrectlyFormatsRequestForJournal()
        {
            var addends = TestHelper.GeneratePositiveNumbers();
            var expected = string.Join(" + ", addends);
            var request = new AddRequest
            {
                Addends = addends
            };

            var formattedRequest = request.GetFormatedRequest();

            formattedRequest.Should().Be(expected);
        }

        [Fact]
        public void AddRequest_CorrectlyReturnsOperationname()
        {
            var expected = "sum";
            var request = new AddRequest();

            var operationName = request.GetOperationName();

            operationName.Should().Be(expected);
        }

        [Fact]
        public void AddResponse_CorrectlyFormatsResponseForJournal()
        {
            var sum = TestHelper.GeneratePositiveNumber();
            var expected = sum.ToString();
            var response = new AddResponse
            {
                Sum = sum
            };

            var formattedResponse = response.GetFormatedResponse();

            formattedResponse.Should().Be(expected);
        }

        [Fact]
        public void AddRequestValidator_WithOneAddend_IsSuccessful()
        {
            var addends = TestHelper.GeneratePositiveNumbers(1);
            var request = new AddRequest
            {
                Addends = addends
            };
            var validator = new AddRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void AddRequestValidator_WithMultipleAddends_IsSuccessful()
        {
            var addends = TestHelper.GeneratePositiveNumbers();
            var request = new AddRequest
            {
                Addends = addends
            };
            var validator = new AddRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void AddRequestValidator_WithNoAddends_IsNotSuccessful()
        {
            var request = new AddRequest
            {
                Addends = Enumerable.Empty<int>()
            };
            var validator = new AddRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }
    }
}
