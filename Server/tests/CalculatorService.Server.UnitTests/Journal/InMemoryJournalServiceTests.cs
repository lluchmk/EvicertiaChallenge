using CalculatorService.Server.Application.DTOs;
using CalculatorService.Server.Application.Journal.Interfaces;
using CalculatorService.Server.Application.Journal.Services;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CalculatorService.Server.Tests.Unit.Journal
{
    public class InMemoryJournalServiceTests
    {
        private readonly IJournalService _sut;
        private readonly ConcurrentDictionary<string, ICollection<JournalOperation>> _journal;

        public InMemoryJournalServiceTests()
        {
            _journal = new();
            _sut = new InMemoryJournalService(_journal);
        }

        [Fact]
        public void RegisterOperation_CorrectlyRegistersOperation()
        {
            var trackId = "trackId";
            var request = TestHelper.GenerateRequest();
            var response = TestHelper.GenerateResponse();
            var date = DateTime.UtcNow;
            var expectedOperation = request.GetOperationName();
            var expectedCalculation = $"{request.GetFormatedRequest()} = {response.GetFormatedResponse()}";

            _sut.RegisterOperation(trackId, request, response, date);

            using (new AssertionScope())
            {
                _journal.TryGetValue(trackId, out var trackJournal).Should().BeTrue();
                trackJournal.Should().ContainSingle();
                var registeredOperation = trackJournal.Single();
                registeredOperation.Operation.Should().Be(expectedOperation);
                registeredOperation.Calculation.Should().Be(expectedCalculation);
                registeredOperation.Date.Should().Be(date);
            }
        }

        [Fact]
        public void RegisterOperation_RegistersMultipleJournals()
        {
            var trackId = "trackId";
            var expectedOperations = 5;
            var requests = TestHelper.GenerateRequests();
            var responses = TestHelper.GenerateResponses();

            for (int i = 0; i < expectedOperations; i++)
            {
                _sut.RegisterOperation(trackId, requests.ElementAt(i), responses.ElementAt(i), DateTime.UtcNow);
            }

            using (new AssertionScope())
            {
                _journal.TryGetValue(trackId, out var trackJournal).Should().BeTrue();
                trackJournal.Should().HaveCount(expectedOperations);
            }
        }

        [Fact]
        public void RegisterOperation_WhenJournalUnexisting_StartsJournal()
        {
            var trackId = "trackId";
            var journalRequest = new JournalRequest { Id = trackId };
            var registeredOperations = _sut.GetJournal(journalRequest);
            Assert.False(registeredOperations.Operations.Any());
            var request = TestHelper.GenerateRequest();
            var response = TestHelper.GenerateResponse();

            _sut.RegisterOperation(trackId, request, response, DateTime.UtcNow);

            using (new AssertionScope())
            {
                _journal.TryGetValue(trackId, out var trackJournal).Should().BeTrue();
                trackJournal.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void GetJournal_WhenJournalFound_ReturnsAllOperations()
        {
            var trackId = "trackId";
            var operationsAmount = 5;
            var requests = TestHelper.GenerateRequests(operationsAmount);
            var responses = TestHelper.GenerateResponses(operationsAmount);

            for (int i = 0; i < operationsAmount; i++)
            {
                _sut.RegisterOperation(trackId, requests.ElementAt(i), responses.ElementAt(i), DateTime.Now);
            }

            using (new AssertionScope())
            {
                _journal.TryGetValue(trackId, out var trackJournal).Should().BeTrue();
                for (int i = 0; i < trackJournal.Count(); i++)
                {
                    trackJournal.ElementAt(i).Operation.Should().Be(requests.ElementAt(i).GetOperationName());
                }
            }
        }

        [Fact]
        public void GetJournal_WhenNoJournalExisting_ReturnsEmptyList()
        {
            var trackId = "1234";

            using (new AssertionScope())
            {
                _journal.TryGetValue(trackId, out var trackJournal).Should().BeFalse();
            }
        }
    }
}
