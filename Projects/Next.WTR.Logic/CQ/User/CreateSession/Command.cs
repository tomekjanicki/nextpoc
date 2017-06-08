namespace Next.WTR.Logic.CQ.User.CreateSession
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<Guid, Error>>
    {
        private Command(NonEmptyString userId, NonEmptyString password, Guid commandId)
            : base(commandId)
        {
            UserId = userId;
            Password = password;
        }

        public NonEmptyString UserId { get; }

        public NonEmptyString Password { get; }

        public static IResult<Command, NonEmptyString> TryCreate(string userId, string password)
        {
            return TryCreate(userId, password, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(string userId, string password, Guid commandId)
        {
            var userIdResult = NonEmptyString.TryCreate(userId, (NonEmptyString)nameof(UserId));
            var passwordResult = NonEmptyString.TryCreate(password, (NonEmptyString)nameof(Password));

            var result = new IResult<NonEmptyString>[]
            {
                userIdResult,
                passwordResult,
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(userIdResult.Value, passwordResult.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && UserId == other.UserId && Password == other.Password;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { UserId, Password, CommandId }.ToImmutableList());
        }
    }
}
