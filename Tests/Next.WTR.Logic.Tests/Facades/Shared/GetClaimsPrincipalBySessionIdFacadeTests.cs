namespace Next.WTR.Logic.Tests.Facades.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.User.GetData;
    using Next.WTR.Logic.Facades.Shared;
    using Next.WTR.Logic.Helpers.QueryCommandFactories.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using NSubstitute;
    using NUnit.Framework;
    using Shouldly;

    public class GetClaimsPrincipalBySessionIdFacadeTests
    {
        private GetClaimsPrincipalBySessionIdFacade _getClaimsPrincipalBySessionIdFacade;
        private IMediator _mediator;
        private IUserGetDataQueryFactory _userGetDataQueryFactory;

        [SetUp]
        public void SetUp()
        {
            _mediator = Substitute.For<IMediator>();
            _userGetDataQueryFactory = Substitute.For<IUserGetDataQueryFactory>();
            _getClaimsPrincipalBySessionIdFacade = new GetClaimsPrincipalBySessionIdFacade(_mediator, _userGetDataQueryFactory);
        }

        [Test]
        public void GetClaimsPrincipal_WrongQuery_ShouldFail()
        {
            const string error = "Error";

            _userGetDataQueryFactory.Get(Arg.Any<string>()).Returns(GetInvalidQueryResult((NonEmptyString)error));

            var result = _getClaimsPrincipalBySessionIdFacade.GetClaimsPrincipal((NonEmptyString)Guid.NewGuid().ToString());

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.Generic);

            result.Error.Message.Value.ShouldBe(error);
        }

        [Test]
        public void GetClaimsPrincipal_DataNotFound_ShouldFail()
        {
            var sessionId = (NonEmptyString)Guid.NewGuid().ToString();

            _userGetDataQueryFactory.Get(Arg.Any<string>()).Returns(GetValidQueryResult(sessionId));

            var result = _getClaimsPrincipalBySessionIdFacade.GetClaimsPrincipal(sessionId);

            result.IsFailure.ShouldBeTrue();

            result.Error.ErrorType.ShouldBe(ErrorType.NotFound);
        }

        [Test]
        public void GetClaimsPrincipal_DataFound_ShouldSucceed()
        {
            var sessionId = (NonEmptyString)Guid.NewGuid().ToString();

            _userGetDataQueryFactory.Get(Arg.Any<string>()).Returns(GetValidQueryResult(sessionId));

            _mediator.Send(Arg.Any<Query>()).Returns(GetUserData());

            var result = _getClaimsPrincipalBySessionIdFacade.GetClaimsPrincipal(sessionId);

            result.IsSuccess.ShouldBeTrue();
        }

        private static IResult<Query, NonEmptyString> GetValidQueryResult(string sessionId)
        {
            var result = Query.TryCreate(sessionId);

            result.EnsureIsNotFaliure();

            return result;
        }

        private static IResult<Query, NonEmptyString> GetInvalidQueryResult(NonEmptyString message)
        {
            return message.GetFailResult<Query>();
        }

        private static UserData GetUserData()
        {
            return new UserData((NonNegativeInt)1, (NonEmptyString)"ln", new List<NonEmptyString> { (NonEmptyString)"Manager", (NonEmptyString)"Finance" }.ToImmutableList());
        }
    }
}
