using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Application.Base.Queries;
using EcommerceDomainDrivenDesign.Application.EventSourcing.EventHistoryData;
using EcommerceDomainDrivenDesign.Application.EventSourcing;
using EcommerceDomainDrivenDesign.Domain;

namespace EcommerceDomainDrivenDesign.Application.Customers.ListCustomerEventHistory
{
    public class ListCustomerEventsQueryHandler : IQueryHandler<ListCustomerEventHistoryQuery, IList<CustomerHistoryData>>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;

        public ListCustomerEventsQueryHandler(
            IEcommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<CustomerHistoryData>> Handle(ListCustomerEventHistoryQuery request, CancellationToken cancellationToken)
        {
            IList<CustomerHistoryData> categoryHistoryData = new List<CustomerHistoryData>();
            var storedEvents = await _unitOfWork.MessageRepository.GetByAggregateId(request.CustomerId, cancellationToken);

            CustomerEventNormatizer normatizer = new CustomerEventNormatizer();
            categoryHistoryData = normatizer.ToHistoryData(storedEvents);
            return categoryHistoryData;
        }
    }
}
