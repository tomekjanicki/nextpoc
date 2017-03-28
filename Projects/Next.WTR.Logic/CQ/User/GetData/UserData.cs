namespace Next.WTR.Logic.CQ.User.GetData
{
    using System.Collections.Immutable;
    using Next.WTR.Types;

    public class UserData
    {
        public UserData(NonNegativeInt userId, NonEmptyString userName, ImmutableList<NonEmptyString> groups)
            : this()
        {
            UserId = userId;
            UserName = userName;
            Groups = groups;
        }

        private UserData()
        {
        }

        public NonNegativeInt UserId { get; }

        public NonEmptyString UserName { get; }

        public ImmutableList<NonEmptyString> Groups { get; }
    }
}