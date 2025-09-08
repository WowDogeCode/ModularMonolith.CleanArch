using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.Orders.DTOs.Requests;
using Orders.Application.Orders.DTOs.Responses;
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
        public async Task<ActionResult<PlaceOrderResponseDto>> PlaceOrder([FromBody] PlaceOrderRequestDto placeOrder)
        {
            var command = new PlaceOrderCommand
            {
                EmployeeId = placeOrder.EmployeeId,
                CustomerId = placeOrder.CustomerId,
                ShipVia = placeOrder.ShipVia,
                RequiredDate = placeOrder.RequiredDate,
                ShippedDate = placeOrder.ShippedDate,
                Freight = placeOrder.Freight,
                ShipName = placeOrder.ShipName,
                ShipAddress = placeOrder.ShipAddress,
                ShipCity = placeOrder.ShipCity,
                ShipRegion = placeOrder.ShipRegion,
                ShipPostalCode = placeOrder.ShipPostalCode,
                ShipCountry = placeOrder.ShipCountry,
                OrderDetails = placeOrder.OrderDetails
            };

            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
