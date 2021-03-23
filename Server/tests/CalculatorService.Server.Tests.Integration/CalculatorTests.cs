using CalculatorService.Server.Application.Calculator.Add;
using CalculatorService.Server.Application.Calculator.Div;
using CalculatorService.Server.Application.Calculator.Mult;
using CalculatorService.Server.Application.Calculator.Sqrt;
using CalculatorService.Server.Application.Calculator.Sub;
using CalculatorService.Server.Application.DTOs;
using CaltulatorService.Server;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Integration
{
    public class CalculatorTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CalculatorTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Add_GetsCorrectResponse()
        {
            var addends = new[] { 4, 567, 23 };
            var expected = addends.Aggregate((acc, x) => acc += x);
            var request = new AddRequest
            {
                Addends = addends
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/add", request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var responseObject = await response.GetResponse<AddResponse>();
                responseObject.Sum.Should().Equals(expected);
            }
        }

        [Fact]
        public async Task Add_WithTrackingId_TracksOperation()
        {
            var request = new AddRequest
            {
                Addends = new[] { 4, 567, 23 }
            };
            var trackId = "Add_WithTrackingId_TracksOperation";
            var client = _factory.CreateClient()
                .WithTrackIdHeader(trackId);

            await client.PostJsonAsync("/calculator/add", request);

            using (new AssertionScope())
            {
                _factory.Journal.TryGetValue(trackId, out var journalOperations)
                    .Should().BeTrue();
                journalOperations.Should().ContainSingle()
                    .Which.Operation.Should().Be(request.GetOperationName());
            }
        }

        [Fact]
        public async Task Add_WithEmptyAddends_Returns400Response()
        {
            var request = new AddRequest();
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/add", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Sub_GetsCorrectResponse()
        {
            var minuend = 10;
            var subtrahend = 5;
            var expected = minuend - subtrahend;
            var request = new SubRequest
            {
                Minuend = minuend,
                Subtrahend = subtrahend
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/sub", request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var responseObject = await response.GetResponse<SubResponse>();
                responseObject.Difference.Should().Equals(expected);
            }
        }

        [Fact]
        public async Task Sub_WithTrackingId_TracksOperation()
        {
            var request = new SubRequest();
            var trackId = "Sub_WithTrackingId_TracksOperation";
            var client = _factory.CreateClient()
                .WithTrackIdHeader(trackId);

            await client.PostJsonAsync("/calculator/sub", request);

            using (new AssertionScope())
            {
                _factory.Journal.TryGetValue(trackId, out var journalOperations)
                    .Should().BeTrue();
                journalOperations.Should().ContainSingle()
                    .Which.Operation.Should().Be(request.GetOperationName());
            }
        }

        [Fact]
        public async Task Mult_GetsCorrectResponse()
        {
            var factors = new[] { 4, 567, 23 };
            var expected = factors.Aggregate((acc, x) => acc *= x);
            var request = new MultRequest
            {
                Factors = factors
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/mult", request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var responseObject = await response.GetResponse<MultResponse>();
                responseObject.Product.Should().Equals(expected);
            }
        }

        [Fact]
        public async Task Mult_WithTrackingId_TracksOperation()
        {
            var request = new MultRequest
            {
                Factors = new[] { 4, 567, 23 }
            };
            var trackId = "Mult_WithTrackingId_TracksOperation";
            var client = _factory.CreateClient()
                .WithTrackIdHeader(trackId);

            await client.PostJsonAsync("/calculator/mult", request);

            using (new AssertionScope())
            {
                _factory.Journal.TryGetValue(trackId, out var journalOperations)
                    .Should().BeTrue();
                journalOperations.Should().ContainSingle()
                    .Which.Operation.Should().Be(request.GetOperationName());
            }
        }

        [Fact]
        public async Task Mult_WithEmptyFactors_Returns400Response()
        {
            var request = new MultRequest();
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/mult", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Div_GetsCorrectResponse()
        {
            var dividend = 10;
            var divisor = 3;
            var expectedQuotient = dividend / divisor;
            var expectedRemainder = dividend % divisor;
            var request = new DivRequest
            {
                Dividend = dividend,
                Divisor = divisor
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/div", request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var responseObject = await response.GetResponse<DivResponse>();
                responseObject.Quotient.Should().Equals(expectedQuotient);
                responseObject.Remainder.Should().Equals(expectedRemainder);
            }
        }

        [Fact]
        public async Task Div_WithTrackingId_TracksOperation()
        {
            var request = new DivRequest
            {
                Dividend = 10,
                Divisor = 3
            };
            var trackId = "Div_WithTrackingId_TracksOperation";
            var client = _factory.CreateClient()
                .WithTrackIdHeader(trackId);

            await client.PostJsonAsync("/calculator/div", request);

            using (new AssertionScope())
            {
                _factory.Journal.TryGetValue(trackId, out var journalOperations)
                    .Should().BeTrue();
                journalOperations.Should().ContainSingle()
                    .Which.Operation.Should().Be(request.GetOperationName());
            }
        }

        [Fact]
        public async Task Div_WithDivisor0_Returns400Response()
        {
            var request = new DivRequest()
            {
                Divisor = 0
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/div", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Sqrt_GetsCorrectResponse()
        {
            var number = 9;
            var expected = 3;
            var request = new SqrtRequest
            {
                Number = number
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/sqrt", request);

            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                var responseObject = await response.GetResponse<SqrtResponse>();
                responseObject.Square.Should().Equals(expected);
            }
        }

        [Fact]
        public async Task Sqrt_WithTrackingId_TracksOperation()
        {
            var request = new SqrtRequest();
            var trackId = "Sqrt_WithTrackingId_TracksOperation";
            var client = _factory.CreateClient()
                .WithTrackIdHeader(trackId);

            await client.PostJsonAsync("/calculator/sqrt", request);

            using (new AssertionScope())
            {
                _factory.Journal.TryGetValue(trackId, out ICollection<JournalOperation> journalOperations)
                    .Should().BeTrue();
                journalOperations.Should().ContainSingle()
                    .Which.Operation.Should().Be(request.GetOperationName());
            }
        }

        [Fact]
        public async Task Sqrt_WithNumberLowerThan0_Returns400Response()
        {
            var request = new SqrtRequest()
            {
                Number = -1
            };
            var client = _factory.CreateClient();

            var response = await client.PostJsonAsync("/calculator/sqrt", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
