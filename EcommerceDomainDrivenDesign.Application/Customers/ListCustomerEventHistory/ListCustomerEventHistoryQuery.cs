using System;
using System.Collections.Generic;
using EcommerceDomainDrivenDesign.Application.Base.Queries;
using EcommerceDomainDrivenDesign.Application.EventSourcing.EventHistoryData;

namespace EcommerceDomainDrivenDesign.Application.Customers.ListCustomerEventHistory
{
    public class ListCustomerEventHistoryQuery : IQuery<IList<CustomerHistoryData>>
    {
        public ListCustomerEventHistoryQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }
}
