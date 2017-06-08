namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Common.Facades;
    using Common.Handlers.Interfaces;
    using Common.Shared;
    using CQ.Customer.Insert;
    using Types;
    using Types.FunctionalExtensions;
    using Web.Dtos.Apis.Customer.Post;

    public sealed class CustomersPostFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CustomersPostFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<int, Error> Post(RequestCustomer requestCustomer)
        {
            var commandResult = Command.TryCreate(requestCustomer.Name, requestCustomer.Code, requestCustomer.Price);

            return Helper.Insert<int, Command, PositiveInt>(_mediator, _mapper, commandResult);
        }
    }
}