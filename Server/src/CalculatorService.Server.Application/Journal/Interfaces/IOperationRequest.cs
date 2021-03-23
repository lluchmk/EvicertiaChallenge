namespace CalculatorService.Server.Application.Journal.Interfaces
{
    public interface IOperationRequest
    {
        string GetOperationName();
        string GetFormatedRequest();
    }
}
