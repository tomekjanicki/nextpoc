namespace Next.WTR.Common.Tests.Mappings.TypeConverters
{
    using AutoMapper;
    using Next.WTR.Common.Mappings;
    using Next.WTR.Common.Mappings.TypeConverters;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using NUnit.Framework;
    using Shouldly;

    public class MaybeClassToStructConvertTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = Helper.GetMapper(Configure);
        }

        [Test]
        public void ConverterWithNullValueShouldWork()
        {
            var source = new From(null);

            var result = _mapper.Map<To>(source);

            result.Value.ShouldBeNull();
        }

        [Test]
        public void ConverterWithNonNullValueShouldWork()
        {
            const int value = 5;

            var source = new From((PositiveInt)value);

            var result = _mapper.Map<To>(source);

            result.Value.ShouldBe(value);
        }

        private static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<From, To>();
            expression.CreateMap<Maybe<PositiveInt>, int?>().ConvertUsing(new MaybeClassToStructConvert<PositiveInt, int>());
        }

        public class From
        {
            public From(Maybe<PositiveInt> value)
            {
                Value = value;
            }

            public Maybe<PositiveInt> Value { get; }
        }

        public class To
        {
            public int? Value { get; set; }
        }
    }
}