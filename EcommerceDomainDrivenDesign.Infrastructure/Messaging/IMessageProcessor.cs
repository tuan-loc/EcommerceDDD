using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Infrastructure.Messaging
{
    public interface IMessageProcessor
    {
        Task ProcessMessages(int batchSize, CancellationToken cancellationToken);
    }
}
