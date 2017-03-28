namespace Next.WTR.Logic.Facades.Apis
{
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.Update;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Product.Update;

    public sealed class ProductsUpdateFacade
    {
        private readonly IMediator _mediator;

        public ProductsUpdateFacade(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IResult<Error> Update(int id, Product product)
        {
            var commandResult = Command.TryCreate(id, product.Version.IfNullReplaceWithEmptyString(), product.Price, product.Name.IfNullReplaceWithEmptyString());

            return Helper.Update(_mediator, commandResult);
        }
    }
}