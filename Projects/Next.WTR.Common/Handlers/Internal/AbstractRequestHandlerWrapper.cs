namespace Next.WTR.Common.Handlers.Internal
{
    using Next.WTR.Common.Handlers.Interfaces;

    internal abstract class AbstractRequestHandlerWrapper<TResult>
    {
        public abstract TResult Handle(IRequest<TResult> message);
    }
}