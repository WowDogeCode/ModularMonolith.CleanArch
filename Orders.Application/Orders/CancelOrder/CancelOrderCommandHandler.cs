using Common.Application.Abstraction;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Orders.Application.Abstraction.Repositories;
using Orders.Application.Orders.DTOs.Responses;
using Orders.Domain.Enums;

namespace Orders.Application.Orders.CancelOrder
{
    public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, CancelOrderResponseDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IValidator<CancelOrderCommand> _validator;
        private readonly ILogger<CancelOrderCommandHandler> _logger;
        public CancelOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IOrderRepository orderRepository,
            IInventoryService inventoryService,
            IValidator<CancelOrderCommand> validator,
            ILogger<CancelOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _inventoryService = inventoryService;
            _validator = validator;
            _logger = logger;
        }
        public async Task<CancelOrderResponseDto> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid is false)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                throw new ApplicationException($"Order with id {request.OrderId} not found.");
            }

            if (order.OrderStatus == OrderStatus.Delivered)
            {
                throw new ApplicationException($"Order with id {request.OrderId} is already delivered and cannot be cancelled.");
            }
            else if (order.OrderStatus == OrderStatus.Shipped)
            {
                throw new ApplicationException($"Order with id {request.OrderId} is already shipped and cannot be cancelled.");
            }
            else if (order.OrderStatus == OrderStatus.Cancelled)
            {
                throw new ApplicationException($"Order with id {request.OrderId} is already cancelled.");
            }

            foreach (var orderDetail in order.OrderDetails)
            {
                bool response = await _inventoryService.IncreaseStockAsync(orderDetail.ProductId, orderDetail.Quantity, cancellationToken);

                if (response is false)
                {
                    _logger.LogWarning("Failed to increase stock for product with id {ProductId} and quantity {Quantity}.", orderDetail.ProductId, orderDetail.Quantity);
                }
            }

            DateTime cancelledDate = order.CancelOrder();

            await _unitOfWork.CommitAsync(cancellationToken);

            return new CancelOrderResponseDto
            {
                OrderId = order.Id,
                CancelledDate = cancelledDate
            };
        }
    }
}

