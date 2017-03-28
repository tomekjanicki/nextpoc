namespace Next.WTR.Logic.CQ.User.GetData
{
    using System;
    using Next.WTR.Types;

    public class AttributesDto
    {
        public AttributesDto(DateTime? lastSeen, NonNegativeInt userId, NonEmptyString userName)
            : this()
        {
            LastSeen = lastSeen;
            UserId = userId;
            UserName = userName;
        }

        private AttributesDto()
        {
        }

        public DateTime? LastSeen { get; }

        public NonNegativeInt UserId { get; }

        public NonEmptyString UserName { get; }
    }
}
