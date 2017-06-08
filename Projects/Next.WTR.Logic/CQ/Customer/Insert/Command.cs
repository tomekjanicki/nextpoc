namespace Next.WTR.Logic.CQ.Customer.Insert
{
    using System;
    using System.Collections.Immutable;
    using Common.CQ;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using Types;
    using Types.FunctionalExtensions;
    using ValueObjects;

    public sealed class Command : BaseCommandQuery<Command>, IRequest<IResult<PositiveInt, Error>>
    {
        private Command(Name name, Code code, NonNegativeDecimal price, Guid commandId)
            : base(commandId)
        {
            Name = name;
            Code = code;
            Price = price;
        }

        public Name Name { get; }

        public Code Code { get; }

        public NonNegativeDecimal Price { get; }

        public static IResult<Command, NonEmptyString> TryCreate(string name, string code, decimal? price)
        {
            return TryCreate(name, code, price, Guid.NewGuid());
        }

        public static IResult<Command, NonEmptyString> TryCreate(string name, string code, decimal? price, Guid commandId)
        {
            var nameResult = Name.TryCreate(name, (NonEmptyString)nameof(Name));
            var codeResult = Code.TryCreate(code, (NonEmptyString)nameof(Code));
            var priceResult = NonNegativeDecimal.TryCreate(price, (NonEmptyString)nameof(Price));

            var result = new IResult<NonEmptyString>[]
            {
                codeResult,
                priceResult,
                nameResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => GetOkResult(new Command(nameResult.Value, codeResult.Value, priceResult.Value, commandId)));
        }

        protected override bool EqualsCore(Command other)
        {
            return base.EqualsCore(other) && Name == other.Name && Code == other.Code && Price == other.Price;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { Name, Code, Price, CommandId }.ToImmutableList());
        }
    }
}
