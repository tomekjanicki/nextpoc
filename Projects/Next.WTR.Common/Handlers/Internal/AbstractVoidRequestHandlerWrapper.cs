﻿namespace Next.WTR.Common.Handlers.Internal
{
    using Interfaces;

    internal abstract class AbstractVoidRequestHandlerWrapper
    {
        public abstract void Handle(IRequest message);
    }
}