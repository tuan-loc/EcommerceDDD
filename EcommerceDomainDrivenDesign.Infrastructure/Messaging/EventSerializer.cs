using System;
using System.Text.Json;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;

namespace EcommerceDomainDrivenDesign.Infrastructure.Messaging
{
    public class EventSerializer : IEventSerializer
    {
        public string Serialize<TE>(TE @event) where TE : Event
        {
            if (null == @event)
                throw new ArgumentNullException(nameof(@event));
            var eventType = @event.GetType();
            var result = JsonSerializer.Serialize(@event, eventType);
            return result;
        }
    }
}
