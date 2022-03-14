using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Queries;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.Modules.Payments.Application.PriceListItems.GetPriceListItems
{
    internal class GetPriceListItemsQueryHandler : IQueryHandler<GetPriceListItemsQuery, List<PriceListItemDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPriceListItemsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<PriceListItemDto>> Handle(GetPriceListItemsQuery request, CancellationToken cancellationToken)
        {
            return await PriceListFactory.GetPriceListItems(_sqlConnectionFactory.GetOpenConnection());
        }
    }
}