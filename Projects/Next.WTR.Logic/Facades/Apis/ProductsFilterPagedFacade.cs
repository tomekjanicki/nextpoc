namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Dtos;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.FilterPaged;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Product.FilterPaged;

    public sealed class ProductsFilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsFilterPagedFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<Paged<ResponseProduct>, Error> FilterPaged(int skip, int top, string filter, string orderBy)
        {
            var queryResult = Query.TryCreate(orderBy, skip, top, filter);

            return Helper.GetItems<Paged<ResponseProduct>, Query, Common.ValueObjects.Paged<Product>>(_mediator, _mapper, queryResult);
        }
    }
}