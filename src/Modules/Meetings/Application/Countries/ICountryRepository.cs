namespace CompanyName.MyMeetings.Modules.Meetings.Application.Countries;

public interface ICountryRepository
{
    Task AddAsync(CountryDto meeting);

    Task<CountryDto> GetByIdAsync(Guid id);

    Task<List<CountryDto>> GetAllCountries();
}