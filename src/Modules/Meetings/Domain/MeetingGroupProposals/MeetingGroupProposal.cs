using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.SharedKernel;

using DomainPack.Contracts.EntitiesContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposal : EntityObjectBase<Guid>, IAggregateRoot
    {
        private string _name;

        private string _description;

        private MeetingGroupLocation _location;

        private DateTime _proposalDate;

        private Guid _proposalUserId;

        private MeetingGroupProposalStatus _status;

        public MeetingGroup CreateMeetingGroup()
        {
            return MeetingGroup.CreateBasedOnProposal(this.Id, _name, _description, _location, _proposalUserId);
        }

        private MeetingGroupProposal(
            string name,
            string description,
            MeetingGroupLocation location,
            Guid proposalUserId)
            : base(Guid.NewGuid())
        {
            _name = name;
            _description = description;
            _location = location;
            _proposalUserId = proposalUserId;
            _proposalDate = SystemClock.Now;
            _status = MeetingGroupProposalStatus.InVerification;

            this.AddDomainEvent(new MeetingGroupProposedDomainEvent(this.Id, _name, _description, proposalUserId, _proposalDate, _location.City, _location.CountryCode));
        }

        public static MeetingGroupProposal ProposeNew(
            string name,
            string description,
            MeetingGroupLocation location,
            Guid proposalMemberId)
        {
            return new MeetingGroupProposal(name, description, location, proposalMemberId);
        }

        public void Accept()
        {
            this.CheckRule(new MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(_status));

            _status = MeetingGroupProposalStatus.Accepted;

            this.AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(this.Id));
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}