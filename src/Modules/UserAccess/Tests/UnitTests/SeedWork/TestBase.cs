﻿using NUnit.Framework;

using System;
using System.Linq;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UnitTests.SeedWork
{
    public abstract class TestBase
    {
        public static T AssertPublishedDomainEvent<T>(EntityObjectBase aggregate)
            where T : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper.GetAllDomainEvents(aggregate).OfType<T>().SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(T).Name} event not published");
            }

            return domainEvent;
        }

        public static void AssertBrokenRule<TRule>(TestDelegate testDelegate)
            where TRule : class, IBusinessRule
        {
            var message = $"Expected {typeof(TRule).Name} broken rule";
            var businessRuleValidationException = Assert.Catch<BusinessRuleValidationException>(testDelegate, message);
            if (businessRuleValidationException != null)
            {
                Assert.That(businessRuleValidationException.BrokenRule, Is.TypeOf<TRule>(), message);
            }
        }
    }
}