using Orders.Domain.Abstraction;

namespace Orders.Domain.Entities
{
    public class Order : IEntity
    {
        private Order(
            int? employeeId,
            string? customerId,
            int? shipVia,
            DateTime orderDate,
            DateTime? requiredDate,
            DateTime? shippedDate,
            decimal freight,
            string? shipName,
            string? shipAddress,
            string? shipCity,
            string? shipRegion,
            string? shipPostalCode,
            string? shipCountry)
        {
            EmployeeId = employeeId;
            CustomerId = customerId;
            ShipVia = shipVia;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
        }
        public int Id { get; private set; }
        public int? EmployeeId { get; private set; }
        public string? CustomerId { get; private set; } // CustomerId type set to string, because NorthwindDB holds CustomerId as string
        public int? ShipVia { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime? RequiredDate { get; private set; }
        public DateTime? ShippedDate { get; private set; }
        public decimal Freight { get; private set; }
        public string? ShipName { get; private set; }
        public string? ShipAddress { get; private set; }
        public string? ShipCity { get; private set; }
        public string? ShipRegion { get; private set; }
        public string? ShipPostalCode { get; private set; }
        public string? ShipCountry { get; private set; }

        private readonly List<OrderDetail> _orderDetails = new();
        public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails.AsReadOnly();

        public static Order Create(
            int? employeeId,
            string? customerId,
            int? shipVia,
            DateTime? requiredDate,
            DateTime? shippedDate,
            decimal? freight,
            string? shipName,
            string? shipAddress,
            string? shipCity,
            string? shipRegion,
            string? shipPostalCode,
            string? shipCountry,
            List<OrderDetail> orderDetails)
        {
            if (orderDetails == null || orderDetails.Count == 0)
            {
                throw new ArgumentException("An order must contain at least one order detail");
            }

            Order order = new Order(
                employeeId,
                customerId,
                shipVia,
                DateTime.UtcNow,
                requiredDate,
                shippedDate,
                freight ?? 0m,
                shipName,
                shipAddress,
                shipCity,
                shipRegion,
                shipPostalCode,
                shipCountry);

            order._orderDetails.AddRange(orderDetails);

            return order;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            if(orderDetail == null)
            {
                throw new ArgumentNullException(nameof(orderDetail));
            }

            orderDetail.SetOrder(this);
            _orderDetails.Add(orderDetail);
        }
    }
}
