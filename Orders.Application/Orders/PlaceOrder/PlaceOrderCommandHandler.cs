using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Orders.Application.Abstraction.Repositories;
using Orders.Application.Orders.DTOs.Responses;
using Orders.Domain.Entities;

namespace Orders.Application.Orders.PlaceOrder
{
    public sealed class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PlaceOrderResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<PlaceOrderCommand> _validator;
        private readonly IInventoryService _inventoryService;
        private readonly IUnitOfWork _unitOfWork;
        public PlaceOrderCommandHandler(IOrderRepository orderRepository, IValidator<PlaceOrderCommand> validator, IInventoryService inventoryService, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _validator = validator;
            _inventoryService = inventoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlaceOrderResponseDto> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var orderDetails = request.OrderDetails.Select(dto => new OrderDetail(dto.ProductId, dto.UnitPrice, dto.Quantity, dto.Discount)).ToList();

            var productsInventoryInfo = await _inventoryService.GetProductInventorySnapshotsAsync(orderDetails.Select(od => od.ProductId).ToList(), cancellationToken);

            if (productsInventoryInfo.Any(x => x.Discontinued == true))
                throw new ValidationException("One or more products are discontinued and cannot be ordered");

            if (orderDetails.Select(od => od.ProductId).Except(productsInventoryInfo.Select(x => x.ProductId)).Any())
                throw new ValidationException("One or more products in the order details do not exist in inventory");

            var orderDetailsDict = orderDetails.ToDictionary(od => od.ProductId, od => od);

            try
            {
                var orderToAdd = Order.Create(request.EmployeeId, request.CustomerId, request.ShipVia,
                request.RequiredDate, null, request.Freight, request.ShipName, request.ShipAddress,
                request.ShipCity, request.ShipRegion, request.ShipPostalCode, request.ShipCountry,
                orderDetails);

                await _orderRepository.AddAsync(orderToAdd);

                foreach (var item in productsInventoryInfo)
                {
                    if (!orderDetailsDict.TryGetValue(item.ProductId, out var orderDetail))
                        continue;

                    bool result;

                    if (item.UnitsInStock < orderDetail.Quantity)
                    {
                        if (item.UnitsInStock + item.UnitsOnOrder < orderDetail.Quantity)
                            throw new ValidationException($"Not enough stock for product {item.ProductName}. Available: {item.UnitsInStock}, On Order: {item.UnitsOnOrder}, Required: {orderDetail.Quantity}");

                        result = await _inventoryService.ReduceStockAsync(item.ProductId, (short)item.UnitsInStock, cancellationToken).ConfigureAwait(false);

                        if (result == false)
                            throw new ValidationException($"Failed to reserve available stock for product {item.ProductName}. Please try again.");

                        continue;
                    }

                    result = await _inventoryService.ReduceStockAsync(item.ProductId, (short)(orderDetail.Quantity), cancellationToken).ConfigureAwait(false);

                    if (!result)
                        throw new ValidationException($"Failed to reserve stock for product {item.ProductName}. Please try again.");
                }

                await _unitOfWork.CommitAsync(cancellationToken);

                return new PlaceOrderResponseDto
                {
                    OrderId = orderToAdd.Id,
                    CustomerId = orderToAdd.CustomerId,
                    OrderDate = orderToAdd.OrderDate,
                    TotalAmount = orderToAdd.OrderDetails.Sum(x => x.Quantity * x.UnitPrice),
                    ItemsCount = orderToAdd.OrderDetails.Count()
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
