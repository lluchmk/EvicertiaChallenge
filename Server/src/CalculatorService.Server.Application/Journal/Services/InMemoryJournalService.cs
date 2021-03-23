using CalculatorService.Server.Application.DTOs;
using CalculatorService.Server.Application.Journal.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CalculatorService.Server.Application.Journal.Services
{
    public class InMemoryJournalService : IJournalService
    {
        private readonly ConcurrentDictionary<string, ICollection<JournalOperation>> _journal;

        public InMemoryJournalService(ConcurrentDictionary<string, ICollection<JournalOperation>> journal)
        {
            _journal = journal;
        }

        public void RegisterOperation(string trackId, IOperationRequest request, IOperationResponse response, DateTime date)
        {
            var journalFound = _journal.TryGetValue(trackId, out var journalOperations);
            if (!journalFound)
            {
                journalOperations = new List<JournalOperation>();
            }

            var calculation = $"{request.GetFormatedRequest()} = {response.GetFormatedResponse()}";
            var operation = new JournalOperation
            {
                Operation = request.GetOperationName(),
                Calculation = calculation,
                Date = date
            };

            journalOperations.Add(operation);
            _journal.TryAdd(trackId, journalOperations);
        }

        public JournalResponse GetJournal(JournalRequest request)
        {
            var journalFound = _journal.TryGetValue(request.Id, out var journalOperations);
            if (!journalFound)
            {
                return new JournalResponse();
            }

            return new JournalResponse
            {
                Operations = journalOperations
            };
        }
    }
}
