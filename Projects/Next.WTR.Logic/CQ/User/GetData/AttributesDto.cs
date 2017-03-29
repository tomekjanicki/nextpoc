namespace Next.WTR.Logic.CQ.User.GetData
{
    using System;
    using Next.WTR.Types;

    public class AttributesDto
    {
        public AttributesDto(DateTime? lastSeen, PositiveInt userId, NonEmptyString userName)
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

        public PositiveInt UserId { get; }

        public NonEmptyString UserName { get; }
    }
}
