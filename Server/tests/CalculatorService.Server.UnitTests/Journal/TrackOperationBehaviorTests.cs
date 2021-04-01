using CalculatorService.Server.Application.Journal.Behaviors;
using CalculatorService.Server.Application.Journal.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Journal
{
    public class TrackOperationBehaviorTests
    {
        private readonly TrackOperationBehavior<IOperationRequest, IOperationResponse> _sut;
        private readonly Mock<IJournalService> _journalServiceMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public TrackOperationBehaviorTests()
        {
            _journalServiceMock = new Mock<IJournalService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _sut = new TrackOperationBehavior<IOperationRequest, IOperationResponse>(_journalServiceMock.Object, _httpContextAccessorMock.Object);
        }

        [Fact]
        public async Task Handle_WhenTrackIdHeaderPresent_RegistersOperation()
        {
            var trackIdHeader = "TrakId";
            _httpContextAccessorMock.Setup(a => a.HttpContext.Request.Headers["X-Evi-Tracking-Id"])
                .Returns(trackIdHeader);

            var request = TestHelper.GenerateRequest();
            var response = TestHelper.GenerateResponse();
            RequestHandlerDelegate<IOperationResponse> requestHandler = () => Task.FromResult(response);

            await _sut.Handle(request, default, requestHandler);

            _journalServiceMock.Verify(j =>
                j.RegisterOperation(trackIdHeader, request, response, It.IsAny<DateTime>()));
        }

        [Fact]
        public async Task Handle_WhenTrackIdHeaderNotPreset_DoesNotRegisterOperation()
        {
            _httpContextAccessorMock.Setup(a => a.HttpContext.Request.Headers["X-Evi-Tracking-Id"])
                .Returns(string.Empty);

            RequestHandlerDelegate<IOperationResponse> requestHandler = () => Task.FromResult((IOperationResponse)default);

            await _sut.Handle(default, default, requestHandler);

            _journalServiceMock.Verify(j =>
                j.RegisterOperation(It.IsAny<string>(), It.IsAny<IOperationRequest>(), It.IsAny<IOperationResponse>(), It.IsAny<DateTime>()),
                Times.Never);
        }
    }
}
