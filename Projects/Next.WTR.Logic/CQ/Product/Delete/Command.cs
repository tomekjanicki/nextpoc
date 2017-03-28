namespace Next.WTR.Logic.CQ.Product.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Next.WTR.Common.CQ;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;
    using Next.WTR.Common.ValueObjects;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion, Guid commandId)
            : base(commandId)
        {
            IdVersion = idVersion;
        }

        public IdVersion IdVersion { get; }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version)
        {
            return TryCreate(id, version, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version, Guid commandId)
        {
            var result = IdVersion.TryCreate(id, version, (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Id), (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Version));
            return result.OnSuccess(() => GetOkResult(new Command(result.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && IdVersion == other.IdVersion;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { IdVersion, CommandId }.ToImmutableList());
        }
    }
}
