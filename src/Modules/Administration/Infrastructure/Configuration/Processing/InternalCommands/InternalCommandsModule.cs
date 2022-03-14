using Autofac;

using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;

using DomainPack.Contracts.CQSContracts;
using DomainPack.CQS.InternalCommands;

using Module = Autofac.Module;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class InternalCommandsModule : Module
    {
        private readonly BiDictionary<string, Type> _internalCommandsMap;

        public InternalCommandsModule(BiDictionary<string, Type> internalCommandsMap)
        {
            _internalCommandsMap = internalCommandsMap;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InternalCommandsMapper>()
                .As<IInternalCommandsMapper>()
                .FindConstructorsWith(new AllConstructorFinder())
                .WithParameter("internalCommandsMap", _internalCommandsMap)
                .SingleInstance();

            CheckMappings();
        }

        private void CheckMappings()
        {
            List<Type> internalCommands = Assemblies.Application
                .GetTypes()
                .Where(x => x.BaseType != null &&
                            (
                                (x.BaseType.IsGenericType &&
                                x.BaseType.GetGenericTypeDefinition() == typeof(InternalCommandBase<>)) ||
                                x.BaseType == typeof(InternalCommandBase)))
                .ToList();

            List<Type> notMappedInternalCommands = new List<Type>();
            foreach (Type internalCommand in internalCommands)
            {
                _internalCommandsMap.TryGetBySecond(internalCommand, out string name);

                if (name == null)
                {
                    notMappedInternalCommands.Add(internalCommand);
                }
            }

            if (notMappedInternalCommands.Any())
            {
                throw new ApplicationException($"Internal Commands {notMappedInternalCommands.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
            }
        }
    }
}