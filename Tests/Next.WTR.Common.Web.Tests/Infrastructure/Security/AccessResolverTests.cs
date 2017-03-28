namespace Next.WTR.Common.Web.Tests.Infrastructure.Security
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.Web.Infrastructure.Security;
    using Next.WTR.Common.Web.Infrastructure.Security.Interface;
    using Next.WTR.Types;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class AccessResolverTests
    {
        private const string Role1 = "Role1";
        private const string Role2 = "Role2";
        private const string AnonymousOrAuthenticated = "AnonymousOrAuthenticated";
        private const string AuthenticatedInRole = "AuthenticatedInRole";
        private const string Authenticated = "Authenticated";
        private AccessResolver _accessResolver;
        private IAccessConfigurationMapProvider _accessConfigurationMapProvider;

        [SetUp]
        public void SetUp()
        {
            _accessConfigurationMapProvider = Substitute.For<IAccessConfigurationMapProvider>();
            _accessResolver = new AccessResolver(_accessConfigurationMapProvider);
        }

        [Test]
        public void CanAccess_NotOnAccessConfigurationMapAuthenticated_ShouldBeTrue()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)Authenticated, true, new List<NonEmptyLowerCaseString>().ToImmutableList());
            result.ShouldBeTrue();
        }

        [Test]
        public void CanAccess_NotOnAccessConfigurationMapNotAuthenticated_ShouldBeFalse()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)Authenticated, false, new List<NonEmptyLowerCaseString>().ToImmutableList());
            result.ShouldBeFalse();
        }

        [Test]
        public void CanAccess_OnAccessConfigurationMapEmptyRolesAuthenticated_ShouldBeTrue()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)AnonymousOrAuthenticated, true, new List<NonEmptyLowerCaseString>().ToImmutableList());
            result.ShouldBeTrue();
        }

        [Test]
        public void CanAccess_OnAccessConfigurationMapEmptyRolesNotAuthenticated_ShouldBeTrue()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)AnonymousOrAuthenticated, false, new List<NonEmptyLowerCaseString>().ToImmutableList());
            result.ShouldBeTrue();
        }

        [Test]
        public void CanAccess_OnAccessConfigurationMapRolesNotAuthenticatedEmptyRoles_ShouldBeFalse()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)AuthenticatedInRole, false, new List<NonEmptyLowerCaseString>().ToImmutableList());
            result.ShouldBeFalse();
        }

        [Test]
        public void CanAccess_OnAccessConfigurationMapRolesAuthenticatedEmptyRoles_ShouldBeFalse()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)AuthenticatedInRole, true, new List<NonEmptyLowerCaseString>().ToImmutableList());
            result.ShouldBeFalse();
        }

        [Test]
        public void CanAccess_OnAccessConfigurationMapRolesAuthenticatedNotInRole_ShouldBeFalse()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)AuthenticatedInRole, true, new List<NonEmptyLowerCaseString> { (NonEmptyLowerCaseString)Role2 }.ToImmutableList());
            result.ShouldBeFalse();
        }

        [Test]
        public void CanAccess_OnAccessConfigurationMapRolesAuthenticatedInRole_ShouldBeTrue()
        {
            SetupAccessConfigurationMapProvider();
            var result = _accessResolver.CanAccess((NonEmptyLowerCaseString)AuthenticatedInRole, true, new List<NonEmptyLowerCaseString> { (NonEmptyLowerCaseString)Role1 }.ToImmutableList());
            result.ShouldBeTrue();
        }

        private void SetupAccessConfigurationMapProvider()
        {
            var dictionary = new Dictionary<NonEmptyLowerCaseString, ImmutableList<NonEmptyLowerCaseString>>
            {
                { (NonEmptyLowerCaseString)AnonymousOrAuthenticated, new List<NonEmptyLowerCaseString>().ToImmutableList() },
                { (NonEmptyLowerCaseString)AuthenticatedInRole, new List<NonEmptyLowerCaseString> { (NonEmptyLowerCaseString)Role1 }.ToImmutableList() }
            }.ToImmutableDictionary();

            _accessConfigurationMapProvider.Get().Returns(dictionary);
        }
    }
}
