using EcommerceDomainDrivenDesign.Application.Base.Queries;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Application.Customers.AuthenticateCustomer
{
    public class AuthenticateCustomerQueryHandler : IQueryHandler<AuthenticateCustomerQuery, CustomerViewModel>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtService _jwtService;

        public AuthenticateCustomerQueryHandler(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ICustomerRepository customerRepository,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _customerRepository = customerRepository;
            _jwtService = jwtService;
        }

        public async Task<CustomerViewModel> Handle(AuthenticateCustomerQuery request, CancellationToken cancellationToken)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();

            var signIn = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);
            if (signIn.Succeeded)
            {
                var token = await _jwtService.GenerateJwt(request.Email);
                var user = await _userManager.FindByEmailAsync(request.Email);

                //Customer data
                var customer = await _customerRepository.GetCustomerByEmail(user.Email);
                customerViewModel.Id = customer.Id;
                customerViewModel.Name = customer.Name;
                customerViewModel.Email = user.Email;
                customerViewModel.Token = token;
                customerViewModel.LoginSucceeded = signIn.Succeeded;
            }
            else
                customerViewModel.ValidationResult.Errors.Add(new ValidationFailure(string.Empty,
                    "Usename or password invalid"));

            return customerViewModel;
        }
    }
}
