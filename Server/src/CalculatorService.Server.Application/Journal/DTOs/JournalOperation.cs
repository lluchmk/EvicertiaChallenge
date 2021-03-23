using System;

namespace CalculatorService.Server.Application.DTOs
{
    public class JournalOperation
    {
        public string Operation { get; set; }
        public string Calculation { get; set; }
        public DateTime Date { get; set; }
    }
}
