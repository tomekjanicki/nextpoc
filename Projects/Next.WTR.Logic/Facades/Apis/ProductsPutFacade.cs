namespace Next.WTR.Logic.Facades.Apis
{
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.Update;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Product.Put;

    public sealed class ProductsPutFacade
    {
        private readonly IMediator _mediator;

        public ProductsPutFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Put(int id, RequestProduct requestProduct)
        {
            var commandResult = Command.TryCreate(id, requestProduct.Version, requestProduct.Price, requestProduct.Name);

            return Helper.Update(_mediator, commandResult);
        }
    }
}