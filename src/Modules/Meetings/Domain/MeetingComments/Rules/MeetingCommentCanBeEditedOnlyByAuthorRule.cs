
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using DomainPack.Contracts.ValidationContracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingComments.Rules
{
    public class MeetingCommentCanBeEditedOnlyByAuthorRule : IBusinessRule
    {
        private readonly Guid _authorId;
        private readonly Guid _editorId;

        public MeetingCommentCanBeEditedOnlyByAuthorRule(Guid authorId, Guid editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }

        public bool IsBroken() => _editorId != _authorId;

        public string Message => "Only the author of a comment can edit it.";
    }
}