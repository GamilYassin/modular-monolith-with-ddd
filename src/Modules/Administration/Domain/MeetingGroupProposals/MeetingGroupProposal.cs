using System;

using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Administration.Domain.Users;

using DomainPack.Contracts.EntitiesContracts;
using DomainPack.Entities;

namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    public class MeetingGroupProposal : EntityObjectBase<Guid>, IAggregateRoot
    {
        private string _name;

        private string _description;

        private MeetingGroupLocation _location;

        private DateTime _proposalDate;

        private Guid _proposalUserId;

        private MeetingGroupProposalStatus _status;

        private MeetingGroupProposalDecision _decision;


        private MeetingGroupProposal(
            Guid id,
            string name,
            string description,
            MeetingGroupLocation location,
            Guid proposalUserId,
            DateTime proposalDate)
            : base(id)
        {
            Id = id;
            _name = name;
            _description = description;
            _location = location;
            _proposalUserId = proposalUserId;
            _proposalDate = proposalDate;

            _status = MeetingGroupProposalStatus.ToVerify;
            _decision = MeetingGroupProposalDecision.NoDecision;

            this.AddDomainEvent(new MeetingGroupProposalVerificationRequestedDomainEvent(this.Id));
        }

        private MeetingGroupProposal()
            : base(default)
        {
        }

        public void Accept(Guid userId)
        {
            this.CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));

            _decision = MeetingGroupProposalDecision.AcceptDecision(DateTime.UtcNow, userId);

            _status = _decision.GetStatusForDecision();

            this.AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(this.Id));
        }

        public void Reject(Guid userId, string rejectReason)
        {
            this.CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(_decision));
            this.CheckRule(new MeetingGroupProposalRejectionMustHaveAReasonRule(rejectReason));

            _decision = MeetingGroupProposalDecision.RejectDecision(DateTime.UtcNow, userId, rejectReason);

            _status = _decision.GetStatusForDecision();

            this.AddDomainEvent(new MeetingGroupProposalRejectedDomainEvent(this.Id));
        }

        public static MeetingGroupProposal CreateToVerify(
            Guid meetingGroupProposalId,
            string name,
            string description,
            MeetingGroupLocation location,
            Guid proposalUserId,
            DateTime proposalDate)
        {
            MeetingGroupProposal meetingGroupProposal = new MeetingGroupProposal(
                Guid.NewGuid(),
                name,
                description,
                location,
                proposalUserId,
                proposalDate);

            return meetingGroupProposal;
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
