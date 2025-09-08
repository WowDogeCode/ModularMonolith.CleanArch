namespace Orders.Application.Orders.DTOs.Requests
{
    public sealed class PlaceOrderRequestDto
    {
        public string? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ShipVia { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        public List<PlaceOrderDetailRequestDto> OrderDetails { get; set; } = new();
    }
}
