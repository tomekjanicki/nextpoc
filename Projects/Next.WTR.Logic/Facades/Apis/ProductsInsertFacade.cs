namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.Insert;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using Next.WTR.Web.Dtos.Apis.Product.Insert;

    public sealed class ProductsInsertFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsInsertFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<int, Error> Insert(RequestProduct requestProduct)
        {
            var commandResult = Command.TryCreate(requestProduct.Name, requestProduct.Code, requestProduct.Price);

            return Helper.Insert<int, Command, PositiveInt>(_mediator, _mapper, commandResult);
        }
    }
}