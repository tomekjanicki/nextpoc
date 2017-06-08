namespace Next.WTR.Common.Dtos
{
    using NullGuard;

    public static class StringExtensions
    {
        public static string IfNullReplaceWithEmptyString([AllowNull]this string input)
        {
            return input ?? string.Empty;
        }
    }
}
