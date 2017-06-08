namespace Next.WTR.Common.ValueObjects
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Globalization;
    using Types;
    using Types.FunctionalExtensions;

    public sealed class DateTimePeriod : ValueObject<DateTimePeriod>
    {
        private DateTimePeriod(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }

        public DateTime From { get; }

        public DateTime To { get; }

        private static NonEmptyString DefaultDateTimeFormatString => (NonEmptyString)"yyyy-MM-dd HH:mm:ss";

        public static IResult<DateTimePeriod, NonEmptyString> TryCreate(string from, string to)
        {
            var fromResult = GetParsedDateTime(from, DefaultDateTimeFormatString, (NonEmptyString)nameof(From));
            var toResult = GetParsedDateTime(to, DefaultDateTimeFormatString, (NonEmptyString)nameof(To));

            var result = new IResult<NonEmptyString>[]
            {
                fromResult,
                toResult
            }.IfAtLeastOneFailCombineElseReturnOk();

            return result.OnSuccess(() => TryCreate(fromResult.Value, toResult.Value));
        }

        public static IResult<DateTimePeriod, NonEmptyString> TryCreate(DateTime from, DateTime to)
        {
            return from > to ? GetFailResult((NonEmptyString)(nameof(From) + " cannot be greater than " + nameof(To))) : GetOkResult(new DateTimePeriod(from, to));
        }

        protected override bool EqualsCore(DateTimePeriod other)
        {
            return From == other.From && To == other.To;
        }

        protected override int GetHashCodeCore()
        {
            return GetCalculatedHashCode(new List<object> { From, To }.ToImmutableList());
        }

        private static IResult<DateTime, NonEmptyString> GetParsedDateTime(string dateTime, NonEmptyString format, NonEmptyString field)
        {
            DateTime result;
            return DateTime.TryParseExact(dateTime, format, null, DateTimeStyles.None, out result) ? result.GetOkMessage() : ((NonEmptyString)("Unable to convert to datetime value from " + field)).GetFailResult<DateTime>();
        }
    }
}
