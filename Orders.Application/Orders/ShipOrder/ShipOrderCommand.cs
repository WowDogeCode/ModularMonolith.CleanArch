using MediatR;
using Orders.Application.Orders.DTOs.Responses;

namespace Orders.Application.Orders.ShipOrder
{
    public record ShipOrderCommand : IRequest<ShipOrderResponseDto>
    {
        public int OrderId { get; set; }
    }
}
