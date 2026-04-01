using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain.Payments;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDomainDrivenDesign.Infrastructure.Domain.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly EcommerceDDDContext _context;

        public PaymentRepository(EcommerceDDDContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddPayment(Payment payment, CancellationToken cancellationToken = default)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<Payment> GetPaymentById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
