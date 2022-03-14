using System;


namespace CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings
{
    public class Term : ValueObjectBase
    {
        public static Term NoTerm => new Term(null, null);

        public DateTime? StartDate { get; }

        public DateTime? EndDate { get; }

        public static Term CreateNewBetweenDates(DateTime? startDate, DateTime? endDate)
        {
            return new Term(startDate, endDate);
        }

        private Term(DateTime? startDate, DateTime? endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        internal bool IsInTerm(DateTime date)
        {
            bool left = !this.StartDate.HasValue || this.StartDate.Value <= date;

            bool right = !this.EndDate.HasValue || this.EndDate.Value >= date;

            return left && right;
        }
    }
}