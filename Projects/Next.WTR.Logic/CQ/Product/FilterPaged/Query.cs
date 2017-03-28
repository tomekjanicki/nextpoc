namespace Next.WTR.Logic.CQ.Product.FilterPaged
{
    using System;
    using System.Collections.Immutable;
    using Next.WTR.Common.CQ;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Common.ValueObjects;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class Query : BaseCommandQuery<Query>, IRequest<Paged<Product>>
    {
        private Query(string name, string code, OrderByTopSkip orderByTopSkip, Guid commandId)
            : base(commandId)
        {
            Name = name;
            Code = code;
            OrderByTopSkip = orderByTopSkip;
        }

        public OrderByTopSkip OrderByTopSkip { get; }

        public string Name { get; }

        public string Code { get; }

        public static IResult<Query, NonEmptyString> TryCreate(string orderBy, int skip, int top, string filter)
        {
            return TryCreate(orderBy, skip, top, filter, Guid.NewGuid());
        }

        public static IResult<Query, NonEmptyString> TryCreate(string orderBy, int skip, int top, string filter, Guid id)
        {
            // todo filter parser
            var orderByParseResult = OrderByParser.TryParse(orderBy, Columns.GetAllowedColumns());

            return orderByParseResult.
                OnSuccess(orderBys => OrderByTopSkip.TryCreate(orderBys, skip, top, (NonEmptyString)nameof(TopSkip.Top), (NonEmptyString)nameof(TopSkip.Skip))).
                OnSuccess(orderByTopSkip => GetOkResult(new Query(string.Empty, string.Empty, orderByTopSkip, id)));
        }

        protected override bool EqualsCore(Query other)
        {
            return base.EqualsCore(other) && OrderByTopSkip == other.OrderByTopSkip && Name == other.Name && Code == other.Code;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new object[] { OrderByTopSkip, Name, Code, CommandId }.ToImmutableList());
        }
    }
}
