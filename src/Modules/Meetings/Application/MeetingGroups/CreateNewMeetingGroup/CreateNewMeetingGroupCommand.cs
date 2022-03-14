using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;



namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup
{
    public class CreateNewMeetingGroupCommand : InternalCommandBase
    {
        
        public CreateNewMeetingGroupCommand(Guid id, Guid meetingGroupProposalId)
            : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal Guid MeetingGroupProposalId { get; }
    }
}