namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.Users
{
    public class UserRole : ValueObjectBase
    {
        public static UserRole Member => new UserRole(nameof(Member));

        public string Value { get; }

        private UserRole(string value)
        {
            this.Value = value;
        }
    }
}