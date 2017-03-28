namespace Next.WTR.Logic.CQ.Product.Insert
{
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.Insert.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, IResult<PositiveInt, Error>>
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public IResult<PositiveInt, Error> Handle(Command message)
        {
            var codeExists = _repository.CodeExists(message.Code);

            if (codeExists)
            {
                return ((NonEmptyString)"Code already defined").ToGeneric<PositiveInt>();
            }

            var id = _repository.Insert(message);

            return Result<PositiveInt, Error>.Ok(id);
        }
    }
}
