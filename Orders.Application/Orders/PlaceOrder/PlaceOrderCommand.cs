using MediatR;
using Orders.Application.Orders.DTOs.Requests;
using Orders.Application.Orders.DTOs.Responses;

namespace Orders.Application.Orders.PlaceOrder
{
    public record PlaceOrderCommand : IRequest<PlaceOrderResponseDto>
    {
        public int? EmployeeId { get; init; }
        public string? CustomerId { get; init; }
        public int? ShipVia { get; init; }
        public DateTime? RequiredDate { get; init; }
        public DateTime? ShippedDate { get; init; }
        public decimal? Freight { get; init; }
        public string? ShipName { get; init; }
        public string? ShipAddress { get; init; }
        public string? ShipCity { get; init; }
        public string? ShipRegion { get; init; }
        public string? ShipPostalCode { get; init; }
        public string? ShipCountry { get; init; }
        public List<PlaceOrderDetailRequestDto> OrderDetails { get; init; } = new();
    }
}
