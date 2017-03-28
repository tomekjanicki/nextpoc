namespace Next.WTR.Logic.CQ.Version.Get
{
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Tools.Interfaces;
    using Next.WTR.Types;

    public sealed class QueryHandler : IRequestHandler<Query, NonEmptyString>
    {
        private readonly IAssemblyVersionProvider _assemblyVersionProvider;

        public QueryHandler(IAssemblyVersionProvider assemblyVersionProvider)
        {
            _assemblyVersionProvider = assemblyVersionProvider;
        }

        public NonEmptyString Handle(Query message)
        {
            return (NonEmptyString)_assemblyVersionProvider.Get(message.Assembly).ToString();
        }
    }
}
