using FluentValidation;
using MediatR;
using Orders.Application.Abstraction.Repositories;
using Orders.Domain.Entities;

namespace Orders.Application.Orders.AddOrder
{
    public sealed class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IValidator<AddOrderCommand> _validator;
        public AddOrderCommandHandler(IOrderRepository orderRepository, IValidator<AddOrderCommand> validator)
        {
            _orderRepository = orderRepository;
            _validator = validator;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Order orderToAdd = new Order(
                employeeId: request.EmployeeId,
                customerId: request.CustomerId,
                shipVia: request.ShipVia,
                orderDate: request.OrderDate,
                requiredDate: request.RequiredDate,
                shippedDate: request.ShippedDate,
                freight: request.Freight,
                shipName: request.ShipName,
                shipAddress: request.ShipAddress,
                shipCity: request.ShipCity,
                shipRegion: request.ShipRegion,
                shipPostalCode: request.ShipPostalCode,
                shipCountry: request.ShipCountry
            );

            await _orderRepository.AddAsync(orderToAdd).ConfigureAwait(false);

            return orderToAdd.Id;
        }
    }
}
