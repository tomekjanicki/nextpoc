namespace Next.WTR.Logic.CQ.User.CreateSession
{
    using System;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.User.CreateSession.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class CommandHandler : IRequestHandler<Command, IResult<Guid, Error>>
    {
        private readonly IRepository _repository;

        public CommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public IResult<Guid, Error> Handle(Command message)
        {
            var maybeAttributes = _repository.GetAttributes(message.UserId);

            if (maybeAttributes.HasNoValue)
            {
                return ((NonEmptyString)"The user id and/or password supplied are incorrect.").ToNotFound<Guid>();
            }

            var attributes = maybeAttributes.Value;

            if (attributes.Locked)
            {
                _repository.SaveLastAttempt(attributes.UserNumber, DateTime.Now);
                return ((NonEmptyString)"This account is locked. Please contact your system administrator.").ToGeneric<Guid>();
            }

            if (message.Password != attributes.Password)
            {
                _repository.SaveLastAttempt(attributes.UserNumber, DateTime.Now);
                return ((NonEmptyString)"The user id and/or password supplied are incorrect.").ToNotFound<Guid>();
            }

            var sessionId = Guid.NewGuid();

            _repository.SaveWithNewSession(attributes.UserNumber, sessionId, DateTime.Now, DateTime.Now, attributes.AdUserName);

            return Result<Guid, Error>.Ok(sessionId);
        }
    }
}
