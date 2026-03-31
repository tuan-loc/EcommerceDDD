using FluentValidation.Results;

namespace EcommerceDomainDrivenDesign.Application.Base.Commands
{
    public abstract class Command<TResult> : ICommand<TResult>
    {
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            ValidationResult = new ValidationResult();
        }

        public virtual bool IsValid()
        {
            return ValidationResult.IsValid;
        }
    }
}
