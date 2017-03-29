namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.Get;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Product.Get;

    public sealed class ProductsGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsGetFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<ResponseProduct, Error> Get(int id)
        {
            var queryResult = Query.TryCreate(id);

            return Helper.GetItem<ResponseProduct, Query, Product>(_mediator, _mapper, queryResult);
        }
    }
}