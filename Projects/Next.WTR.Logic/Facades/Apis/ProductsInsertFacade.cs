namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.Insert;
    using Types;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Product.Insert;

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