﻿namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events
{
    public class MeetingGroupProposedDomainEvent : DomainEventBase
    {
        public MeetingGroupProposedDomainEvent(
            Guid meetingGroupProposalId,
            string name,
            string description,
            Guid proposalUserId,
            DateTime proposalDate,
            string locationCity,
            string locationCountryCode)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
            this.Name = name;
            this.Description = description;
            this.LocationCity = locationCity;
            this.LocationCountryCode = locationCountryCode;
            this.ProposalDate = proposalDate;
            this.ProposalUserId = proposalUserId;
        }

        public Guid MeetingGroupProposalId { get; }

        public string Name { get; }

        public string Description { get; }

        public string LocationCity { get; }

        public string LocationCountryCode { get; }

        public Guid ProposalUserId { get; }

        public DateTime ProposalDate { get; }
    }
}