using MediatR;

namespace Orders.Application.Orders.AddOrder
{
    public record AddOrderCommand : IRequest<int>
    {
        public int? EmployeeId { get; init; }
        public string? CustomerId { get; init; }
        public int? ShipVia { get; init; }
        public DateTime? OrderDate { get; init; }
        public DateTime? RequiredDate { get; init; }
        public DateTime? ShippedDate { get; init; }
        public decimal Freight { get; init; }
        public string? ShipName { get; init; }
        public string? ShipAddress { get; init; }
        public string? ShipCity { get; init; }
        public string? ShipRegion { get; init; }
        public string? ShipPostalCode { get; init; }
        public string? ShipCountry { get; init; }
    }
}
