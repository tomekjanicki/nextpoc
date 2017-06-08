namespace Next.WTR.Logic.CQ.Product.ValueObjects
{
    using Types;
    using Types.FunctionalExtensions;

    public sealed class Code : SimpleClassValueObject<Code, string>
    {
        private Code(string value)
            : base(value)
        {
        }

        public static explicit operator Code(string value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator string(Code code)
        {
            return code.Value;
        }

        public static implicit operator NonEmptyString(Code value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonEmptyString.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static IResult<Code, NonEmptyString> TryCreate(string code, NonEmptyString field)
        {
            if (code == string.Empty)
            {
                return GetFailResult((NonEmptyString)"{0} can't be empty", field);
            }

            const int max = 50;

            return code.Length > max ? GetFailResult((NonEmptyString)$"{{0}} can't be longer than {max} chars.", field) : GetOkResult(new Code(code));
        }
    }
}