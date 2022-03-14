using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;

using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup
{
    public class CreateNewMeetingGroupCommand : InternalCommandBase
    {
        [JsonConstructor]
        public CreateNewMeetingGroupCommand(Guid id, MeetingGroupProposalId meetingGroupProposalId)
            : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal MeetingGroupProposalId MeetingGroupProposalId { get; }
    }
}