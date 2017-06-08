namespace Next.WTR.Web
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reflection;
    using System.Web.Http;
    using Common.Cache;
    using Common.Cache.Interfaces;
    using Common.Handlers;
    using Common.Handlers.Interfaces;
    using Common.IoC;
    using Common.Mappings;
    using Common.Shared.TemplateMethods.Commands.Interfaces;
    using Common.Shared.TemplateMethods.Queries.Interfaces;
    using Common.Tools;
    using Common.Tools.Interfaces;
    using Infrastructure;
    using Logic.CQ.Product;
    using Logic.Database;
    using Logic.Database.Interfaces;
    using Logic.Facades.Apis;
    using Logic.Mappings;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.RegisterWebApiControllers(configuration);

            RegisterSingletons(container);

            container.Verify();

            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            IoCContainerProvider.SetContainer(new IoCContainer(container));
        }

        private static void RegisterSingletons(Container container)
        {
            container.RegisterSingleton<IMediator, Mediator>();
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton<IAssemblyVersionProvider, AssemblyVersionProvider>();
            container.RegisterSingleton<IDbConnectionProvider, DbConnectionProvider>();
            container.RegisterSingleton<IEntryAssemblyProvider, EntryAssemblyProvider>();
            container.RegisterSingleton<ICacheProvider, CacheProvider>();
            container.RegisterSingleton(() => Helper.GetMapper(AutoMapperConfiguration.Configure));

            // todo consider go back to scoped lifestyle
            container.RegisterSingleton<Logic.CQ.Product.Delete.Interfaces.IRepository, Logic.CQ.Product.Delete.Repository>();
            container.RegisterSingleton<IUpdateRepository<Logic.CQ.Product.Update.Command>, Logic.CQ.Product.Update.Repository>();
            container.RegisterSingleton<Logic.CQ.Product.Insert.Interfaces.IRepository, Logic.CQ.Product.Insert.Repository>();
            container.RegisterSingleton<Logic.CQ.User.CreateSession.Interfaces.IRepository, Logic.CQ.User.CreateSession.Repository>();
            container.RegisterSingleton<Logic.CQ.User.GetData.Interfaces.IRepository, Logic.CQ.User.GetData.Repository>();
            container.RegisterSingleton<IGetRepository<Logic.CQ.Product.Get.Product>, Logic.CQ.Product.Get.Repository>();
            var concreteTypes = GetConcreteTypes();
            concreteTypes.ForEach(type => container.RegisterSingleton(type, type));
            var lifeStyle = Lifestyle.Singleton;
            var assemblies = GetAssemblies();
            container.Register(typeof(IRequestHandler<,>), assemblies, lifeStyle);
            container.Register(typeof(IVoidRequestHandler<>), assemblies, lifeStyle);
        }

        private static ImmutableList<Type> GetConcreteTypes()
        {
            return new List<Type>
            {
                typeof(ProductsFilterPagedFacade),
                typeof(ProductsGetFacade),
                typeof(VersionGetFacade),
                typeof(ProductsDeleteFacade),
                typeof(ProductsUpdateFacade),
                typeof(ProductsInsertFacade),
                typeof(SharedQueries)
            }.ToImmutableList();
        }

        private static ImmutableList<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(AutoMapperConfiguration).GetTypeInfo().Assembly }.ToImmutableList();
        }
    }
}