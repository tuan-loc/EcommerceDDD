using EcommerceDomainDrivenDesign.Domain.Shared;
using FluentValidation;

namespace EcommerceDomainDrivenDesign.Application.Orders.ChangeOrder
{
    public class ChangeOrderCommandValidator : AbstractValidator<ChangeOrderCommand>
    {
        public ChangeOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty.");
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is empty.");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty.");

            this.RuleFor(x => x.Currency).Must(c => Currency.SupportedCurrencies().Contains(c))
                .WithMessage("At least one product has invalid currency");
        }
    }
}
