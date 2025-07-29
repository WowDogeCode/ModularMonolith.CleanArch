using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Orders.AddOrder;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-order")]
        public async Task<ActionResult<int>> AddOrder([FromBody] AddOrderCommand addOrder)
        {
            int result = await _mediator.Send(addOrder).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
