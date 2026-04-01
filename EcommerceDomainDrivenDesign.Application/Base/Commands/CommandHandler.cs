using EcommerceDomainDrivenDesign.Domain.Core.Base;
using FluentValidation.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Application.Base.Commands
{
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<CommandHandlerResult>
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract Task<Guid> RunCommand(TCommand command, CancellationToken cancellationToken);

        public async Task<CommandHandlerResult> Handle(TCommand command, CancellationToken cancellationToken)
        {
            CommandHandlerResult result = new CommandHandlerResult();

            try
            {
                if ((object)command == null)
                    throw new ArgumentNullException(nameof(command));

                if (command.IsValid())
                    result.Id = await RunCommand(command, cancellationToken);
            }
            catch (BusinessRuleException e)
            {
                command.ValidationResult.Errors.Add(new ValidationFailure("Business rule error", e.Message));
            }
            catch (InvalidDataException e)
            {
                command.ValidationResult.Errors.Add(new ValidationFailure("Invalid data error", e.Message));
            }

            result.ValidationResult = command.ValidationResult;
            return result;
        }
    }
}
