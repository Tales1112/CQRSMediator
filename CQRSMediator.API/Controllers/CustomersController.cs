using CQRSMediator.Application.Commands;
using CQRSMediator.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CQRSMediator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });

            if (result == null)
                return NotFound();
            else
                return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerByIdCommand { Id = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            if(id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
