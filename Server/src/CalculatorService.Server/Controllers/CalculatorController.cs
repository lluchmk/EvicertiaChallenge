using CalculatorService.Server.Application.Calculator.Add;
using CalculatorService.Server.Application.Calculator.Div;
using CalculatorService.Server.Application.Calculator.Mult;
using CalculatorService.Server.Application.Calculator.Sqrt;
using CalculatorService.Server.Application.Calculator.Sub;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaltulatorServiceServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add two or more operands and retrieve the result.
        /// </summary>
        /// <param name="request">Contains the operands that will be added.</param>
        /// <returns>The sum of the given operands.</returns>
        [HttpPost("add")]
        [ProducesResponseType(200, Type = typeof(AddResponse))]
        public async Task<IActionResult> Add([FromBody] AddRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Subtrack two or more operands and retrieve the result.
        /// </summary>
        /// <param name="request">Contais the minuend and subtrahend of the operation.</param>
        /// <returns>The difference of the fiven operands.</returns>
        [HttpPost("sub")]
        [ProducesResponseType(200, Type = typeof(SubResponse))]
        public async Task<IActionResult> Sub([FromBody] SubRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Multiply two or more operands and retrieve the result.
        /// </summary>
        /// <param name="request">Conatins the factors the will be multiplied.</param>
        /// <returns>The product of the given operands.</returns>
        [HttpPost("mult")]
        [ProducesResponseType(200, Type = typeof(MultResponse))]
        public async Task<IActionResult> Mult([FromBody] MultRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Divide two or more operands and retrieve the result.
        /// </summary>
        /// <param name="request">Contains the dividend and divisor.</param>
        /// <returns>The quotiend and remainder of the given operands.</returns>
        [HttpPost("div")]
        [ProducesResponseType(200, Type = typeof(DivResponse))]
        public async Task<IActionResult> AddDiv([FromBody] DivRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        /// <summary>
        /// Square root of one operand and retrieve the result.
        /// </summary>
        /// <param name="request">Contins the number for which the square root will be calculated.</param>
        /// <returns>The square root of the given operand.</returns>
        [HttpPost("sqrt")]
        [ProducesResponseType(200, Type = typeof(SqrtResponse))]
        public async Task<IActionResult> Sqrt([FromBody] SqrtRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
