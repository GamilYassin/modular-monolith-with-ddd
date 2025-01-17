﻿using Autofac;

using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using CompanyName.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;
using CompanyName.MyMeetings.Modules.Administration.Application.Members;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Authentication;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.DataAccess;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.EventsBus;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Logging;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Mediation;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Outbox;
using CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Quartz;

using DomainPack.DomainEvents.EventBus;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    public class AdministrationStartup
    {
        private static IContainer _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus)
        {
            //ILogger moduleLogger = logger.ForContext("Module", "Administration");

            ConfigureContainer(connectionString, executionContextAccessor, moduleLogger, eventsBus);

            QuartzStartup.Initialize(moduleLogger);

            EventsBusStartup.Initialize(moduleLogger);
        }

        public static void Stop()
        {
            QuartzStartup.StopQuartz();
        }

        private static void ConfigureContainer(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger,
            IEventsBus eventsBus)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));

            SerilogLoggerFactory loggerFactory = new SerilogLoggerFactory(logger);
            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new ProcessingModule());
            containerBuilder.RegisterModule(new EventsBusModule(eventsBus));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new AuthenticationModule());

            BiDictionary<string, Type> domainNotificationsMap = new BiDictionary<string, Type>();
            domainNotificationsMap.Add("MeetingGroupProposalAcceptedNotification", typeof(MeetingGroupProposalAcceptedNotification));
            containerBuilder.RegisterModule(new OutboxModule(domainNotificationsMap));

            BiDictionary<string, Type> internalCommandsMap = new BiDictionary<string, Type>();
            internalCommandsMap.Add("CreateMember", typeof(CreateMemberCommand));
            internalCommandsMap.Add("RequestMeetingGroupProposalVerification", typeof(RequestMeetingGroupProposalVerificationCommand));
            containerBuilder.RegisterModule(new InternalCommandsModule(internalCommandsMap));

            containerBuilder.RegisterModule(new QuartzModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            AdministrationCompositionRoot.SetContainer(_container);
        }
    }
}