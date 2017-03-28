namespace Next.WTR.Logic.Facades.Apis
{
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.Delete;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class ProductsDeleteFacade
    {
        private readonly IMediator _mediator;

        public ProductsDeleteFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Delete(int id, string version)
        {
            var commandResult = Command.TryCreate(id, version);

            return Helper.Delete(_mediator, commandResult);
        }
    }
}
