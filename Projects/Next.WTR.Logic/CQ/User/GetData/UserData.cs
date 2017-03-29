namespace Next.WTR.Logic.CQ.User.GetData
{
    using System.Collections.Immutable;
    using Next.WTR.Types;

    public class UserData
    {
        public UserData(PositiveInt userId, NonEmptyString userName, ImmutableList<NonEmptyString> groups)
        {
            UserId = userId;
            UserName = userName;
            Groups = groups;
        }

        public PositiveInt UserId { get; }

        public NonEmptyString UserName { get; }

        public ImmutableList<NonEmptyString> Groups { get; }
    }
}