namespace Next.WTR.Common.Tests.Mappings.TypeConverters
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using AutoMapper;
    using Next.WTR.Common.Dtos;
    using Next.WTR.Common.Mappings;
    using Next.WTR.Common.Mappings.TypeConverters;
    using Next.WTR.Types;
    using NUnit.Framework;
    using Shouldly;

    public class PagedConverterTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = Helper.GetMapper(Configure);
        }

        [Test]
        public void ConverterShouldWork()
        {
            var source = Common.ValueObjects.Paged<string>.Create((NonNegativeInt)5, new List<string> { "value" }.ToImmutableList());

            var result = _mapper.Map<Paged<string>>(source);

            result.Count.ShouldBe(source.Count);

            result.Items.Count.ShouldBe(source.Items.Count);

            result.Items.SequenceEqual(source.Items).ShouldBeTrue();
        }

        private static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap(typeof(Common.ValueObjects.Paged<>), typeof(Paged<>)).ConvertUsing(typeof(PagedConverter<,>));
            expression.CreateMap(typeof(IEnumerable<>), typeof(ImmutableList<>)).ConvertUsing(typeof(ImmutableListConverter<,>));
        }
    }
}
