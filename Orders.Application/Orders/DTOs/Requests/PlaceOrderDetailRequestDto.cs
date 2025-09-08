namespace Orders.Application.Orders.DTOs.Requests
{
    public sealed class PlaceOrderDetailRequestDto
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
