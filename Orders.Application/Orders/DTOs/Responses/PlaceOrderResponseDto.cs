namespace Orders.Application.Orders.DTOs.Responses
{
    public sealed class PlaceOrderResponseDto
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public int ItemsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
