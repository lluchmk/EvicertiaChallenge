using CalculatorService.Server.Application.DTOs;
using System;

namespace CalculatorService.Server.Application.Journal.Interfaces
{
    public interface IJournalService
    {
        void RegisterOperation(string trackId, IOperationRequest request, IOperationResponse response, DateTime date);
        JournalResponse GetJournal(JournalRequest request);
    }
}
