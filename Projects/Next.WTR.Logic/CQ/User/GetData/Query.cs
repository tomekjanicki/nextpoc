﻿namespace Next.WTR.Logic.CQ.User.GetData
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.CQ;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

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
            Guid guid;
            var result = Guid.TryParse(sessionId, out guid);
            return result ? GetOkResult(new Query(guid, commandId)) : GetFailResult((NonEmptyString)("Invalid " + nameof(SessionId)));
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
