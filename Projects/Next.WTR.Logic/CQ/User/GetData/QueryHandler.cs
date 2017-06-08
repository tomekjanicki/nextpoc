namespace Next.WTR.Logic.CQ.User.GetData
{
    using System;
    using Common.Handlers.Interfaces;
    using Interfaces;
    using Types.FunctionalExtensions;

    public sealed class QueryHandler : IRequestHandler<Query, Maybe<UserData>>
    {
        private readonly IRepository _repository;

        public QueryHandler(IRepository repository)
        {
            _repository = repository;
        }

        public Maybe<UserData> Handle(Query message)
        {
            var maybeSessionData = _repository.GetSessionDataBySessionId(message.SessionId);

            if (maybeSessionData.HasNoValue)
            {
                return null;
            }

            var sessionData = maybeSessionData.Value;

            if (sessionData.LastSeen == null || !(sessionData.LastSeen >= DateTime.Now.AddMinutes(-30)))
            {
                return null;
            }

            _repository.SaveLastSeen(message.SessionId, DateTime.Now);
            var groups = _repository.GetGroupsByUserId(sessionData.UserId);
            return new UserData(sessionData.UserId, sessionData.UserName, groups);
        }
    }
}
