﻿namespace Next.WTR.Types
{
    using FunctionalExtensions;

    public sealed class PositiveInt : SimpleStructValueObject<PositiveInt, int>
    {
        private PositiveInt(int value)
            : base(value)
        {
        }

        public static explicit operator PositiveInt(int value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator NonNegativeInt(PositiveInt value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonNegativeInt.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator PositiveDecimal(PositiveInt value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => PositiveDecimal.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator NonNegativeDecimal(PositiveInt value)
        {
            return GetValueWhenSuccessOrThrowInvalidCastException(() => NonNegativeDecimal.TryCreate(value, (NonEmptyString)"Value"));
        }

        public static implicit operator int(PositiveInt value)
        {
            return value.Value;
        }

        public static IResult<PositiveInt, NonEmptyString> TryCreate(int? value, NonEmptyString field)
        {
            return TryCreateInt(value, field, v => TryCreate(v, field));
        }

        public static IResult<PositiveInt, NonEmptyString> TryCreate(int value, NonEmptyString field)
        {
            return TryCreateInt(value, (NonEmptyString)(field + " can't be less or equal to zero"), v => v > 0, v => new PositiveInt(v));
        }
    }
}
