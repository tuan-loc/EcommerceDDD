using MediatR;

namespace EcommerceDomainDrivenDesign.Application.Base.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
