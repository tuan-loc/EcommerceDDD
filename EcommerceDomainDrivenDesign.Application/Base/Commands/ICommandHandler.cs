using MediatR;

namespace EcommerceDomainDrivenDesign.Application.Base.Commands
{
    public interface ICommandHandler<in TCommand, TResult> :
        IRequestHandler<TCommand, CommandHandlerResult> where TCommand : ICommand<CommandHandlerResult>
    { }
}
