namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Product.Insert;
    using Types;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Product.Post;

    public sealed class ProductsPostFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsPostFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<int, Error> Post(RequestProduct requestProduct)
        {
            var commandResult = Command.TryCreate(requestProduct.Name, requestProduct.Code, requestProduct.Price);

            return Helper.Insert<int, Command, PositiveInt>(_mediator, _mapper, commandResult);
        }
    }
}