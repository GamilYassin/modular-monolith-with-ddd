﻿using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions
{
    public class MemberSubscriptionExpirationDateChangedNotificationHandler :
        INotificationHandler<MemberSubscriptionExpirationDateChangedNotification>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly ICommandsScheduler _commandsScheduler;

        public MemberSubscriptionExpirationDateChangedNotificationHandler(ISqlConnectionFactory sqlConnectionFactory, ICommandsScheduler commandsScheduler)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(MemberSubscriptionExpirationDateChangedNotification notification, CancellationToken cancellationToken)
        {
            //var sql = "SELECT " +
            //          $"[MeetingGroupMember].MeetingGroupId AS [{nameof(MeetingGroupMemberResponse.MeetingGroupId)}], " +
            //          $"[MeetingGroupMember].RoleCode AS [{nameof(MeetingGroupMemberResponse.RoleCode)}] " +
            //          "FROM [meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember] " +
            //          "WHERE [MeetingGroupMember].MemberId = @MemberId";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            //var meetingGroupMembers = await connection.QueryAsync<MeetingGroupMemberResponse>(
            //    sql,
            //    new
            //    {
            //        MemberId = notification.DomainEvent.MemberId
            //    });

            //var meetingGroupList = meetingGroupMembers.AsList();

            //List<MeetingGroupMemberData> meetingGroups = meetingGroupList
            //    .Select(x =>
            //        new MeetingGroupMemberData(
            //            x.MeetingGroupId,
            //            MeetingGroupMemberRole.Of(x.RoleCode)))
            //    .ToList();

            //var meetingGroupsCoveredByMemberSubscription =
            //    MeetingGroupExpirationDatePolicy.GetMeetingGroupsCoveredByMemberSubscription(meetingGroups);

            //foreach (var meetingGroup in meetingGroupsCoveredByMemberSubscription)
            //{
            //    await _commandsScheduler.EnqueueAsync(new SetMeetingGroupExpirationDateCommand(
            //        Guid.NewGuid(),
            //        meetingGroup,
            //        notification.DomainEvent.ExpirationDate));
            //}

            throw new NotImplementedException();
        }

        private class MeetingGroupMemberResponse
        {
            public Guid MeetingGroupId { get; set; }

            public string RoleCode { get; set; }
        }
    }
}