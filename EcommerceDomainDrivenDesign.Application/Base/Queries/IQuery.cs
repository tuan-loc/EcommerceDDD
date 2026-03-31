using MediatR;

namespace EcommerceDomainDrivenDesign.Application.Base.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}
