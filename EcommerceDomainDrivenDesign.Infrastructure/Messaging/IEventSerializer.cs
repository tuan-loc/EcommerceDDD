using EcommerceDomainDrivenDesign.Domain.Core.Messaging;

namespace EcommerceDomainDrivenDesign.Infrastructure.Messaging
{
    public interface IEventSerializer
    {
        string Serialize<TE>(TE @event) where TE : Event;
    }
}
