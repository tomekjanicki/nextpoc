namespace Next.WTR.Logic.CQ.User.GetData
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Query : BaseCommandQuery<Query>, IRequest<Maybe<UserData>>
    {
        private Query(Guid sessionId, Guid commandId)
            : base(commandId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }

        public static IResult<Query, NonEmptyString> TryCreate(string sessionId)
        {
            return TryCreate(sessionId, Guid.NewGuid());
        }

        public static IResult<Query, NonEmptyString> TryCreate(string sessionId, Guid commandId)
        {
            var sessionIdResult = sessionId.TryParseToGuid((NonEmptyString)nameof(SessionId));
            return sessionIdResult.OnSuccess(() => GetOkResult(new Query(sessionIdResult.Value, commandId)));
        }

        protected override bool EqualsCore(Query other)
        {
            return base.EqualsCore(other) && SessionId == other.SessionId;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { CommandId, SessionId }.ToImmutableList());
        }
    }
}
