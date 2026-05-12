using MediatR;
using Orders.Application.Orders.DTOs.Responses;

namespace Orders.Application.Orders.CancelOrder
{
    public record CancelOrderCommand : IRequest<CancelOrderResponseDto>
    {
        public int OrderId { get; init; }
    }
}
