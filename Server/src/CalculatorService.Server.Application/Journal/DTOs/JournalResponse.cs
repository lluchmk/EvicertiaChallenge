using System.Collections.Generic;
using System.Linq;

namespace CalculatorService.Server.Application.DTOs
{
    public class JournalResponse
    {
        public IEnumerable<JournalOperation> Operations { get; set; } = Enumerable.Empty<JournalOperation>();
    }
}
