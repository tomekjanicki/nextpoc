namespace Next.WTR.Common.Mappings.TypeConverters
{
    using System.Collections.Immutable;
    using AutoMapper;
    using Next.WTR.Common.Dtos;
    using NullGuard;

    public sealed class PagedConverter<TSource, TDestination> : ITypeConverter<Common.ValueObjects.Paged<TSource>, Paged<TDestination>>
    {
        public Paged<TDestination> Convert(Common.ValueObjects.Paged<TSource> source, [AllowNull] Paged<TDestination> destination, ResolutionContext context)
        {
            return new Paged<TDestination>(source.Count, context.Mapper.Map<ImmutableList<TDestination>>(source.Items));
        }
    }
}