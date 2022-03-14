using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Countries;

internal class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, List<CountryDto>>
{
    private readonly ICountryRepository repository;

    public GetAllCountriesQueryHandler(ICountryRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<CountryDto>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
    {
        return await repository.GetAllCountries();
    }
}