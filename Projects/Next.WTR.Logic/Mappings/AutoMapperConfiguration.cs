namespace Next.WTR.Logic.Mappings
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using AutoMapper;
    using Next.WTR.Common.Dtos;
    using Next.WTR.Common.Mappings.TypeConverters;
    using Next.WTR.Web.Dtos.Apis.Product.FilterPaged;

    public static class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression expression)
        {
            expression.CreateMap(typeof(Common.ValueObjects.Paged<>), typeof(Paged<>)).ConvertUsing(typeof(PagedConverter<,>));
            expression.CreateMap(typeof(IEnumerable<>), typeof(ImmutableList<>)).ConvertUsing(typeof(ImmutableListConverter<,>));

            expression.CreateMap<CQ.Product.FilterPaged.Product, ResponseProduct>();
            expression.CreateMap<CQ.Product.Get.Product, Web.Dtos.Apis.Product.Get.ResponseProduct>();
        }
    }
}
