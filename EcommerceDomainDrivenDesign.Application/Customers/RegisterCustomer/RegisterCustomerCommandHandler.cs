using EcommerceDomainDrivenDesign.Application.Base.Commands;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Application.Customers.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : CommandHandler<RegisterCustomerCommand, CommandHandlerResult>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;
        private readonly ICustomerUniquenessChecker _uniquenessChecker;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterCustomerCommandHandler(
            UserManager<User> userManager,
            IEcommerceUnitOfWork unitOfWork,
            ICustomerUniquenessChecker uniquenessChecker,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _uniquenessChecker = uniquenessChecker;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Guid> RunCommand(RegisterCustomerCommand command, CancellationToken cancellationToken)
        {
            Customer customer = Customer.CreateCustomer(command.Email, command.Name, _uniquenessChecker);
            if (customer != null)
            {
                await _unitOfWork.CustomerRepository.RegisterCustomer(customer, cancellationToken);
                if (await _unitOfWork.CommitAsync())
                    await CreateUserForCustomer(command);
            }

            return customer.Id;
        }

        private async Task<User> CreateUserForCustomer(RegisterCustomerCommand request)
        {
            //Creating Identity user
            var user = new User(_httpContextAccessor)
            {
                UserName = request.Email,
                Email = request.Email
            };

            var userCreated = await _userManager.CreateAsync(user, request.Password);
            if (userCreated.Succeeded)
            {
                //Adding user claims
                await _userManager.AddClaimAsync(user, new Claim("CanRead", "Read"));
                await _userManager.AddClaimAsync(user, new Claim("CanSave", "Save"));
                await _userManager.AddClaimAsync(user, new Claim("CanDelete", "Delete"));
            }

            return user;
        }
    }
}
