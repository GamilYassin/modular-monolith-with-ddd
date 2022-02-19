using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.SeedWork;

using NUnit.Framework;

using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Domain.UnitTests.MeetingGroupProposals
{
    [TestFixture]
    public class MeetingGroupProposalTests : TestBase
    {
        [Test]
        public void ProposeNewMeetingGroup_IsSuccessful()
        {
            MemberId proposalMemberId = new MemberId(Guid.NewGuid());

            MeetingGroupProposal meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            MeetingGroupProposedDomainEvent meetingGroupProposed = AssertPublishedDomainEvent<MeetingGroupProposedDomainEvent>(meetingProposal);

            Assert.That(meetingGroupProposed.MeetingGroupProposalId, Is.EqualTo(meetingProposal.Id));
        }

        [Test]
        public void AcceptProposal_WhenIsNotAccepted_IsSuccessful()
        {
            MemberId proposalMemberId = new MemberId(Guid.NewGuid());

            MeetingGroupProposal meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            meetingProposal.Accept();

            MeetingGroupProposalAcceptedDomainEvent meetingGroupProposalAccepted = AssertPublishedDomainEvent<MeetingGroupProposalAcceptedDomainEvent>(meetingProposal);

            Assert.That(meetingGroupProposalAccepted.MeetingGroupProposalId, Is.EqualTo(meetingProposal.Id));
        }

        [Test]
        public void AcceptProposal_WhenIsAlreadyAccepted_BreaksProposalCannotBeAcceptedMoreThanOnceRule()
        {
            MemberId proposalMemberId = new MemberId(Guid.NewGuid());

            MeetingGroupProposal meetingProposal = MeetingGroupProposal.ProposeNew(
                "name",
                "description",
                MeetingGroupLocation.CreateNew("Warsaw", "PL"),
                proposalMemberId);

            meetingProposal.Accept();

            AssertBrokenRule<MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule>(() =>
            {
                meetingProposal.Accept();
            });
        }

        [Test]
        public void CreateMeetingGroup_IsSuccessful_And_CreatorIsAHost()
        {
            MemberId proposalMemberId = new MemberId(Guid.NewGuid());
            string name = "name";
            string description = "description";
            MeetingGroupLocation meetingGroupLocation = MeetingGroupLocation.CreateNew("Warsaw", "PL");
            MeetingGroupProposal meetingProposal = MeetingGroupProposal.ProposeNew(
                name,
                description,
                meetingGroupLocation,
                proposalMemberId);

            MeetingGroup meetingGroup = meetingProposal.CreateMeetingGroup();

            MeetingGroupCreatedDomainEvent meetingGroupCreated = AssertPublishedDomainEvent<MeetingGroupCreatedDomainEvent>(meetingGroup);
            NewMeetingGroupMemberJoinedDomainEvent newMeetingGroupMemberJoined = AssertPublishedDomainEvent<NewMeetingGroupMemberJoinedDomainEvent>(meetingGroup);

            Assert.That(meetingGroupCreated.MeetingGroupId, Is.EqualTo(meetingProposal.Id));
            Assert.That(newMeetingGroupMemberJoined.MemberId, Is.EqualTo(proposalMemberId));
            Assert.That(newMeetingGroupMemberJoined.Role, Is.EqualTo(MeetingGroupMemberRole.Organizer));
        }
    }
}