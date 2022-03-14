using System;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Meetings;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using DomainPack.Contracts.EntitiesContracts;
using DomainPack.Entities;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations
{
    public class MeetingCommentingConfiguration : EntityObjectBase<Guid>, IAggregateRoot
    {

        private Guid _meetingId;

        private bool _isCommentingEnabled;

        private MeetingCommentingConfiguration(Guid meetingId): base(Guid.NewGuid())
        {
            this._meetingId = meetingId;
            this._isCommentingEnabled = true;

            this.AddDomainEvent(new MeetingCommentingConfigurationCreatedDomainEvent(this._meetingId, this._isCommentingEnabled));
        }


        public void EnableCommenting(Guid enablingMemberId, MeetingGroup meetingGroup)
        {
            CheckRule(new MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(enablingMemberId, meetingGroup));

            if (!this._isCommentingEnabled)
            {
                this._isCommentingEnabled = true;
                AddDomainEvent(new MeetingCommentingEnabledDomainEvent(this._meetingId));
            }
        }

        public void DisableCommenting(Guid disablingMemberId, MeetingGroup meetingGroup)
        {
            CheckRule(new MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(disablingMemberId, meetingGroup));

            if (this._isCommentingEnabled)
            {
                this._isCommentingEnabled = false;
                AddDomainEvent(new MeetingCommentingDisabledDomainEvent(this._meetingId));
            }
        }

        public bool GetIsCommentingEnabled() => _isCommentingEnabled;

        internal static MeetingCommentingConfiguration Create(Guid meetingId)
            => new MeetingCommentingConfiguration(meetingId);

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}