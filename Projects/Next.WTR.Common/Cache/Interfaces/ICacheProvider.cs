namespace Next.WTR.Common.Cache.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Types;
    using Types.FunctionalExtensions;

    public interface ICacheProvider
    {
        Maybe<T> GetAndStore<T>(NonEmptyString key, TimeSpan validFor, Func<Maybe<T>> valueFactory)
            where T : class;

        Task<Maybe<T>> GetAndStoreAsync<T>(NonEmptyString key, TimeSpan validFor, Func<Task<Maybe<T>>> valueFactory)
            where T : class;
    }
}