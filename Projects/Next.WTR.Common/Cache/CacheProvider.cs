namespace Next.WTR.Common.Cache
{
    using System;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using Next.WTR.Common.Cache.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class CacheProvider : ICacheProvider
    {
        private readonly ObjectCache _objectCache;

        public CacheProvider()
        {
            _objectCache = MemoryCache.Default;
        }

        public Maybe<T> GetAndStore<T>(NonEmptyString key, TimeSpan validFor, Func<Maybe<T>> valueFactory)
            where T : class
        {
            var newValue = new Lazy<Maybe<T>>(valueFactory);

            var oldValue = _objectCache.AddOrGetExisting(key, newValue, GetDateTimeOffset(validFor)) as Lazy<Maybe<T>>;

            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                _objectCache.Remove(key);
                throw;
            }
        }

        public Task<Maybe<T>> GetAndStoreAsync<T>(NonEmptyString key, TimeSpan validFor, Func<Task<Maybe<T>>> valueFactory)
            where T : class
        {
            var newValue = new Lazy<Task<Maybe<T>>>(valueFactory);

            var oldValue = _objectCache.AddOrGetExisting(key, newValue, GetDateTimeOffset(validFor)) as Lazy<Task<Maybe<T>>>;

            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                _objectCache.Remove(key);
                throw;
            }
        }

        private static DateTimeOffset GetDateTimeOffset(TimeSpan validFor)
        {
            return DateTimeOffset.Now.AddMilliseconds(validFor.TotalMilliseconds);
        }
    }
}
