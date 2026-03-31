using EcommerceDomainDrivenDesign.Application.Base.Queries;

namespace EcommerceDomainDrivenDesign.Application.Customers.AuthenticateCustomer
{
    public class AuthenticateCustomerQuery : IQuery<CustomerViewModel>
    {
        public AuthenticateCustomerQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
