using System;
using System.Collections.Generic;
using System.Linq;
using EcommerceDomainDrivenDesign.Application.EventSourcing.EventHistoryData;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;

namespace EcommerceDomainDrivenDesign.Application.EventSourcing
{
    public class CustomerEventNormatizer : EventNormatizer<CustomerHistoryData>
    {
        public override IList<CustomerHistoryData> ToHistoryData(IList<StoredEvent> messages)
        {
            IList<CustomerHistoryData> historyData = GetHistoryData(messages);
            var sorted = historyData.OrderBy(c => c.Timestamp);
            var categoryHistory = new List<CustomerHistoryData>();
            var last = new CustomerHistoryData();

            foreach (var change in sorted)
            {
                var data = new CustomerHistoryData();
                data.Id = change.Id == Guid.Empty.ToString() ||
                    change.Id == last.Id ? "" : change.Id;

                data.Name = string.IsNullOrWhiteSpace(change.Name) ||
                    change.Name == last.Name ? "" : change.Name;

                data.Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action;
                data.Timestamp = change.Timestamp;

                categoryHistory.Add(data);
                last = change;
            }

            return categoryHistory;
        }

    }
}
