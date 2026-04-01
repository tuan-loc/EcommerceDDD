using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Customers.Orders.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;


namespace EcommerceDomainDrivenDesign.Application.Orders.PlaceOrder
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private IEcommerceUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderPlacedEventHandler(IEcommerceUnitOfWork unitOfWork, IServiceScopeFactory scopeFactory)
        {
            _unitOfWork = unitOfWork;
            _scopeFactory = scopeFactory;
        }

        public async Task Handle(OrderPlacedEvent orderPlacedEvent, CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                _unitOfWork = scope.ServiceProvider.GetRequiredService<IEcommerceUnitOfWork>();

                //Creating a payment                
                var payment = new Payment(orderPlacedEvent.OrderId);
                await _unitOfWork.PaymentRepository.AddPayment(payment);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
