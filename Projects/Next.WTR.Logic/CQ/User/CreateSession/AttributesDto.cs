namespace Next.WTR.Logic.CQ.User.CreateSession
{
    using Next.WTR.Types;

    public class AttributesDto
    {
        public AttributesDto(bool locked, NonEmptyString password, PositiveInt userNumber, NonEmptyString adUserName)
            : this()
        {
            Locked = locked;
            Password = password;
            UserNumber = userNumber;
            AdUserName = adUserName;
        }

        private AttributesDto()
        {
        }

        public bool Locked { get; }

        public NonEmptyString Password { get; }

        public PositiveInt UserNumber { get; }

        public NonEmptyString AdUserName { get; }
    }
}