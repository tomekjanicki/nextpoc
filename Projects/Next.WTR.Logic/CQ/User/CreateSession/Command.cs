namespace Next.WTR.Logic.CQ.User.CreateSession
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.CQ;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Account.Login;

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

        public static IResult<Command, NonEmptyString> TryCreate(Data data)
        {
            return TryCreate(data, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(Data data, Guid commandId)
        {
            var userIdResult = NonEmptyString.TryCreate(data.UserId, (NonEmptyString)nameof(UserId));
            var passwordResult = NonEmptyString.TryCreate(data.Password, (NonEmptyString)nameof(Password));

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
