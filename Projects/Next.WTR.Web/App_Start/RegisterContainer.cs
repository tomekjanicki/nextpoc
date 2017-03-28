namespace Next.WTR.Web
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Reflection;
    using System.Web.Http;
    using Next.WTR.Common.Cache;
    using Next.WTR.Common.Cache.Interfaces;
    using Next.WTR.Common.Handlers;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.IoC;
    using Next.WTR.Common.Mappings;
    using Next.WTR.Common.Security.Interfaces;
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;
    using Next.WTR.Common.Shared.TemplateMethods.Queries.Interfaces;
    using Next.WTR.Common.Tools;
    using Next.WTR.Common.Tools.Interfaces;
    using Next.WTR.Common.Web.Infrastructure.Security;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Logic.CQ.Product;
    using Next.WTR.Logic.Database;
    using Next.WTR.Logic.Database.Interfaces;
    using Next.WTR.Logic.Facades.Apis;
    using Next.WTR.Logic.Facades.Shared;
    using Next.WTR.Logic.Helpers.QueryCommandFactories;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Logic.Mappings;
    using Next.WTR.Web.Infrastructure;
    using Next.WTR.Web.Infrastructure.Security;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class RegisterContainer
    {
        public static void Execute(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

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
            container.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            container.RegisterSingleton<ICacheProvider, CacheProvider>();
            container.RegisterSingleton<IUserGetDataQueryFactory, UserGetDataQueryIdQueryFactory>();
            container.RegisterSingleton<IUserCreateSessionCommandFactory, UserCreateSessionCommandFactory>();
            container.RegisterSingleton<IUserDeleteSessionCommandFactory, UserDeleteSessionCommandFactory>();
            container.RegisterSingleton<IAccessConfigurationMapProvider, AccessConfigurationMapProvider>();
            container.RegisterSingleton<IAccessResolver, AccessResolver>();
            container.RegisterSingleton(() => Helper.GetMapper(AutoMapperConfiguration.Configure));

            // todo consider go back to scoped lifestyle
            container.RegisterSingleton<IClaimsPrincipalProvider, ClaimsPrincipalProvider>();
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
                typeof(GetClaimsPrincipalBySessionIdFacade),
                typeof(ProductsFilterPagedFacade),
                typeof(ProductsGetFacade),
                typeof(VersionGetFacade),
                typeof(ProductsDeleteFacade),
                typeof(ProductsUpdateFacade),
                typeof(ProductsInsertFacade),
                typeof(AccountLoginFacade),
                typeof(AccountLogoutFacade),
                typeof(SharedQueries)
            }.ToImmutableList();
        }

        private static ImmutableList<Assembly> GetAssemblies()
        {
            return new List<Assembly> { typeof(AutoMapperConfiguration).GetTypeInfo().Assembly }.ToImmutableList();
        }
    }
}