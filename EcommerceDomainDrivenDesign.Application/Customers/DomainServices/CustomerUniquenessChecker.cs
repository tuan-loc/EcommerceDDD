using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Customers;

namespace EcommerceDomainDrivenDesign.Application.Customers.DomainServices
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;

        public CustomerUniquenessChecker(IEcommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsUserUnique(string customerEmail)
        {
            var customer = _unitOfWork.CustomerRepository.GetCustomerByEmail(customerEmail).Result;
            return customer == null;
        }
    }
}
