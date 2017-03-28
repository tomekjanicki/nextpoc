namespace Next.WTR.Common.Handlers.Interfaces
{
    public interface IRequestHandler<in TRequest, out TResponse>
        where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest message);
    }
}