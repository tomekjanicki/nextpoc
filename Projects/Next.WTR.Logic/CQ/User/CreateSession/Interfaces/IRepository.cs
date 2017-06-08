namespace Next.WTR.Logic.CQ.User.CreateSession.Interfaces
{
    using System;
    using Types;
    using Types.FunctionalExtensions;

    public interface IRepository
    {
        Maybe<AttributesDto> GetAttributes(NonEmptyString userId);

        void SaveLastAttempt(PositiveInt userNumber, DateTime lastAttempt);

        void SaveWithNewSession(PositiveInt userNumber, Guid sessionId, DateTime lastSeen, DateTime lastAttempt, NonEmptyString adUserName);
    }
}