namespace Next.WTR.Logic.CQ.Product.Get
{
    using Next.WTR.Common.Shared.TemplateMethods.Queries;
    using Next.WTR.Common.Shared.TemplateMethods.Queries.Interfaces;

    public sealed class QueryHandler : GetCommandHandlerTemplate<Query, IGetRepository<Product>, Product>
    {
        public QueryHandler(IGetRepository<Product> repository)
            : base(repository)
        {
        }
    }
}
