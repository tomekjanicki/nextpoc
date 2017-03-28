namespace Next.WTR.Common.Handlers.Internal
{
    using Next.WTR.Common.Handlers.Interfaces;

    internal abstract class AbstractVoidRequestHandlerWrapper
    {
        public abstract void Handle(IRequest message);
    }
}