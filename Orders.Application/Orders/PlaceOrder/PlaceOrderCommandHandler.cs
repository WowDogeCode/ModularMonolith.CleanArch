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
            {
                throw new ValidationException(validationResult.Errors);
            }

            var orderDetails = request.OrderDetails.Select(dto => new OrderDetail(dto.ProductId, dto.UnitPrice, dto.Quantity, dto.Discount)).ToList();

            var orderToAdd = Order.Create(request.EmployeeId, request.CustomerId, request.ShipVia,
                request.RequiredDate, request.ShippedDate, request.Freight, request.ShipName, request.ShipAddress,
                request.ShipCity, request.ShipRegion, request.ShipPostalCode, request.ShipCountry,
                orderDetails);

            foreach (var item in orderToAdd.OrderDetails)
            {
                await _inventoryService.ReduceStockAsync(item.ProductId, item.Quantity, cancellationToken);
            }

            await _orderRepository.AddAsync(orderToAdd);
            await _unitOfWork.CommitAsync(cancellationToken);

            var response = new PlaceOrderResponseDto
            {
                OrderId = orderToAdd.Id,
                CustomerId = orderToAdd.CustomerId,
                OrderDate = orderToAdd.OrderDate,
                TotalAmount = orderToAdd.OrderDetails.Sum(x => x.Quantity * x.UnitPrice),
                ItemsCount = orderToAdd.OrderDetails.Count()
            };

            return response;
        }
    }
}
