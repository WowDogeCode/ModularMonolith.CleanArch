namespace Orders.Domain.Entities
{
    public class OrderDetail
    {
        public int OrderId { get; private set; }
        public int ProductId { get; private set;}
        public decimal UnitPrice {  get; private set; }
        public short Quantity { get; private set; }
        public decimal Discount { get; private set; }

        public Order Order { get; private set; } = default!;

        public OrderDetail(int productId, decimal unitPrice, short quantity, decimal discount)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Discount = discount;
        }
    }
}
