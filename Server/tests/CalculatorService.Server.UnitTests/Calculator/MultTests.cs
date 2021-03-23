using CalculatorService.Server.Application.Calculator.Mult;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Calculator
{
    public class MultTests
    {
        [Fact]
        public async Task MultHandler_MultipliesFactors()
        {
            var factors = TestHelper.GeneratePositiveNumbers();
            var request = new MultRequest
            {
                Factors = factors
            };

            var expected = 1;
            foreach (var Multend in factors)
            {
                expected *= Multend;
            }
            var handler = new MultHandler();

            var response = await handler.Handle(request, default);

            response.Product.Should().Be(expected);
        }

        [Fact]
        public void MultRequest_CorrectlyFormatsRequestForJournal()
        {
            var factors = TestHelper.GeneratePositiveNumbers();
            var expected = string.Join(" * ", factors);
            var request = new MultRequest
            {
                Factors = factors
            };

            var formattedRequest = request.GetFormatedRequest();

            formattedRequest.Should().Be(expected);
        }

        [Fact]
        public void MultRequest_CorrectlyReturnsOperationname()
        {
            var expected = "mult";
            var request = new MultRequest();

            var operationName = request.GetOperationName();

            operationName.Should().Be(expected);
        }

        [Fact]
        public void MultResponse_CorrectlyFormatsResponseForJournal()
        {
            var product = TestHelper.GeneratePositiveNumber();
            var expected = product.ToString();
            var response = new MultResponse
            {
                Product = product
            };

            var formattedResponse = response.GetFormatedResponse();

            formattedResponse.Should().Be(expected);
        }

        [Fact]
        public void MultRequestValidator_WithOneMultend_IsSuccessful()
        {
            var factors = TestHelper.GeneratePositiveNumbers(1);
            var request = new MultRequest
            {
                Factors = factors
            };
            var validator = new MultRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MultRequestValidator_WithMultipleMultends_IsSuccessful()
        {
            var factors = TestHelper.GeneratePositiveNumbers();
            var request = new MultRequest
            {
                Factors = factors
            };
            var validator = new MultRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void MultRequestValidator_WithNoMultends_IsNotSuccessful()
        {
            var request = new MultRequest
            {
                Factors = Enumerable.Empty<int>()
            };
            var validator = new MultRequestValidator();

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
        }
    }
}
