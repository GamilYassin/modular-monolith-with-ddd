using Autofac;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Email
{
    internal class EmailModule : Module
    {
        private readonly EmailsConfiguration _configuration;

        public EmailModule(EmailsConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .WithParameter("configuration", _configuration)
                .InstancePerLifetimeScope();
        }
    }
}