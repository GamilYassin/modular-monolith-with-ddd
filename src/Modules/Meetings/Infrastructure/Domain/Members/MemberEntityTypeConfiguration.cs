﻿using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanyName.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members
{
    internal class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members", "meetings");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_login").HasColumnName("Login");
            builder.Property<string>("_email").HasColumnName("Email");
            builder.Property<string>("_firstName").HasColumnName("FirstName");
            builder.Property<string>("_lastName").HasColumnName("LastName");
            builder.Property<string>("_name").HasColumnName("Name");
        }
    }
}