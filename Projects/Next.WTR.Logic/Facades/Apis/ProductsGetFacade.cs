namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.Get;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Product.Get;

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