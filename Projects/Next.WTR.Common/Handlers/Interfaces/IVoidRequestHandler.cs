namespace Next.WTR.Common.Handlers.Interfaces
{
    public interface IVoidRequestHandler<in TRequest>
        where TRequest : IRequest
    {
        void Handle(TRequest message);
    }
}