using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(StoredEvent message, System.Threading.CancellationToken cancellationToken);
    }
}
