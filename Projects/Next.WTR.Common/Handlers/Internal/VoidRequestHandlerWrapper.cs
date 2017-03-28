﻿namespace Next.WTR.Common.Handlers.Internal
{
    using Next.WTR.Common.Handlers.Interfaces;

    internal sealed class VoidRequestHandlerWrapper<TCommand> : AbstractVoidRequestHandlerWrapper
        where TCommand : IRequest
    {
        private readonly IVoidRequestHandler<TCommand> _inner;

        public VoidRequestHandlerWrapper(IVoidRequestHandler<TCommand> inner)
        {
            _inner = inner;
        }

        public override void Handle(IRequest message)
        {
            _inner.Handle((TCommand)message);
        }
    }
}