

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class MeetingLocation : ValueObjectBase
    {
        public static MeetingLocation CreateNew(string name, string address, string postalCode, string city)
        {
            return new MeetingLocation(name, address, postalCode, city);
        }

        private MeetingLocation(string name, string address, string postalCode, string city)
        {
            Name = name;
            Address = address;
            PostalCode = postalCode;
            City = city;
        }

        public string Name { get; }

        public string Address { get; }

        public string PostalCode { get; }

        public string City { get; }
    }
}