using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Orders.PlaceOrder;

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

        [HttpPost("place-order")]
        public async Task<ActionResult<int>> PlaceOrder([FromBody] PlaceOrderCommand placeOrder)
        {
            int result = await _mediator.Send(placeOrder).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
