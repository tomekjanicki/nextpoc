namespace Next.WTR.Logic.CQ.User.DeleteSession
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.CQ;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Command : BaseCommandQuery<Command>, IRequest
    {
        private Command(Guid sessionId, Guid commandId)
            : base(commandId)
        {
            SessionId = sessionId;
        }

        public Guid SessionId { get; }

        public static IResult<Command, NonEmptyString> TryCreate(string sessionId)
        {
            return TryCreate(sessionId, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(string sessionId, Guid commandId)
        {
            var sessionIdResult = sessionId.TryParseToGuid((NonEmptyString)nameof(SessionId));
            return sessionIdResult.OnSuccess(() => GetOkResult(new Command(sessionIdResult.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && SessionId == other.SessionId;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { SessionId, CommandId }.ToImmutableList());
        }
    }
}
