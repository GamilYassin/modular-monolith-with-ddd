using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

using DomainPack.Contracts.MediatorContracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}