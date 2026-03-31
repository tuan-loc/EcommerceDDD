using AutoMapper;
using EcommerceDomainDrivenDesign.Application.Customers.RegisterCustomer;
using EcommerceDomainDrivenDesign.Application.Customers.UpdateCustomer;

namespace EcommerceDomainDrivenDesign.Application.AutoMapper
{
    public class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<RegisterCustomerRequest, RegisterCustomerCommand>()
            .ConstructUsing(c => new RegisterCustomerCommand(c.Email, c.Name, c.Password, c.PasswordConfirm));

            CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>()
            .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name));
        }
    }
}
