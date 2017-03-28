namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Common.Tools.Interfaces;
    using Next.WTR.Logic.CQ.Version.Get;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class VersionGetFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IEntryAssemblyProvider _entryAssemblyProvider;

        public VersionGetFacade(IMediator mediator, IMapper mapper, IEntryAssemblyProvider entryAssemblyProvider)
        {
            _mediator = mediator;
            _mapper = mapper;
            _entryAssemblyProvider = entryAssemblyProvider;
        }

        public IResult<string, Error> Get()
        {
            var query = Query.Create(_entryAssemblyProvider.GetAssembly());

            return Helper.GetItemSimple<string, Query, NonEmptyString>(_mediator, _mapper, query);
        }
    }
}
