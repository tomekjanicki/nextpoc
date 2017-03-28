namespace Next.WTR.Logic.CQ.User.GetData.Interfaces
{
    using System;
    using System.Collections.Immutable;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public interface IRepository
    {
        Maybe<AttributesDto> GetSessionDataBySessionId(Guid sessionId);

        void SaveLastSeen(Guid sessionId, DateTime lastSeen);

        ImmutableList<NonEmptyString> GetGroupsByUserId(NonNegativeInt userId);
    }
}
