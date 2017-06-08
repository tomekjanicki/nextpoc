namespace Next.WTR.Logic.Facades.Apis
{
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.Delete;
    using Types.FunctionalExtensions;

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
