namespace Next.WTR.Logic.CQ.Product.Update
{
    using System;
    using System.Collections.Immutable;
    using Next.WTR.Common.CQ;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Common.Shared.TemplateMethods.Commands.Interfaces;
    using Next.WTR.Common.ValueObjects;
    using Next.WTR.Logic.CQ.Product.ValueObjects;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<Error>>, IIdVersion
    {
        private Command(IdVersion idVersion, NonNegativeDecimal price, Name name, Guid commandId)
            : base(commandId)
        {
            IdVersion = idVersion;
            Price = price;
            Name = name;
        }

        public IdVersion IdVersion { get; }

        public NonNegativeDecimal Price { get; }

        public Name Name { get; }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version, decimal? price, string name)
        {
            return TryCreate(id, version, price, name, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(int id, string version, decimal? price, string name, Guid commandId)
        {
            var idVersionResult = IdVersion.TryCreate(id, version, (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Id), (NonEmptyString)nameof(Common.ValueObjects.IdVersion.Version));
            var priceResult = NonNegativeDecimal.TryCreate(price, (NonEmptyString)nameof(Price));
            var nameResult = Name.TryCreate(name, (NonEmptyString)nameof(Name));

            var result = new IResult<NonEmptyString>[]
            {
                idVersionResult,
                priceResult,
                nameResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(idVersionResult.Value, priceResult.Value, nameResult.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && IdVersion == other.IdVersion && Price == other.Price && Name == other.Name;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { IdVersion, Price, Name, CommandId }.ToImmutableList());
        }
    }
}
