﻿using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule : IBusinessRule
    {
        private readonly MeetingCommentingConfiguration _meetingCommentingConfiguration;

        public CommentCanBeCreatedOnlyIfCommentingForMeetingEnabledRule(MeetingCommentingConfiguration meetingCommentingConfiguration)
        {
            _meetingCommentingConfiguration = meetingCommentingConfiguration;
        }

        public bool IsBroken() => !_meetingCommentingConfiguration.GetIsCommentingEnabled();

        public string Message => "Commenting for meeting is disabled.";
    }
}