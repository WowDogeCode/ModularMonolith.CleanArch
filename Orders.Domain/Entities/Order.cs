namespace Orders.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; }
        public int? EmployeeId { get; }
        public int? CustomerId { get; }
        public int? ShipVia { get; }
        public DateTime? OrderDate { get; private set; }
        public DateTime? RequiredDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }
        public decimal Freight { get; private set; }
        public string? ShipName { get; private set; }
        public string? ShipAddress { get; private set; }
        public string? ShipCity { get; private set; }
        public string? ShipRegion { get; private set; }
        public string? ShipPostalCode { get; private set; }
        public string? ShipCountry { get; private set; }
    }
}
