namespace Next.WTR.Logic.Facades.Apis
{
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.Update;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Product.Update;

    public sealed class ProductsUpdateFacade
    {
        private readonly IMediator _mediator;

        public ProductsUpdateFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Update(int id, RequestProduct requestProduct)
        {
            var commandResult = Command.TryCreate(id, requestProduct.Version, requestProduct.Price, requestProduct.Name);

            return Helper.Update(_mediator, commandResult);
        }
    }
}