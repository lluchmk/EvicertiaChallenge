using CalculatorService.Server.Application.DTOs;
using CalculatorService.Server.Application.Journal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorService.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        /// <summary>
        /// Request all operations for a Tracking­Id.
        /// </summary>
        /// <param name="request">Contains the Trackingid.</param>
        /// <returns>All the operations for the given TrackingId.</returns>
        [HttpPost("query")]
        [ProducesResponseType(200, Type = typeof(JournalResponse))]
        public IActionResult GetJournal([FromBody] JournalRequest request)
        {
            var response = _journalService.GetJournal(request);
            return Ok(response);
        }
    }
}
