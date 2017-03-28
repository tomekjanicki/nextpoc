namespace Next.WTR.Common.Tests.Cache
{
    using System;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Threading;
    using System.Threading.Tasks;
    using Next.WTR.Common.Cache;
    using Next.WTR.Common.Cache.Interfaces;
    using Next.WTR.Types;
    using Next.WTR.Types.FunctionalExtensions;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using NUnit.Framework;
    using Shouldly;

    public class CacheProviderTests
    {
        private const string Result = "result";

        private const string Key = " cacheKey ";

        private ICacheProvider _cacheProvider;
        private TimeSpan _cacheTimeSpan;

        [SetUp]
        public void SetUp()
        {
            ResetCache();
            _cacheProvider = new CacheProvider();
            _cacheTimeSpan = TimeSpan.FromSeconds(120);
        }

        [Test]
        public async Task GetAndStoreAsync_NonNullValue_ShouldQueryForDataOnlyOnce()
        {
            var func = Substitute.For<Func<Task<Maybe<string>>>>();
            func.Invoke().Returns(Task.FromResult((Maybe<string>)Result));

            var r1 = await _cacheProvider.GetAndStoreAsync((NonEmptyString)Key, _cacheTimeSpan, func).ConfigureAwait(false);
            var r2 = await _cacheProvider.GetAndStoreAsync((NonEmptyString)Key, _cacheTimeSpan, func).ConfigureAwait(false);
            var r3 = await _cacheProvider.GetAndStoreAsync((NonEmptyString)Key, _cacheTimeSpan, func).ConfigureAwait(false);

            r1.HasValue.ShouldBeTrue();
            r2.HasValue.ShouldBeTrue();
            r3.HasValue.ShouldBeTrue();

            r1.Value.ShouldBe(Result);
            r2.Value.ShouldBe(Result);
            r3.Value.ShouldBe(Result);

            await func.Received(1).Invoke().ConfigureAwait(false);
        }

        [Test]
        public void GetAndStore_NonNullValue_ShouldQueryForDataOnce()
        {
            var func = Substitute.For<Func<Maybe<string>>>();
            func.Invoke().Returns(Result);

            var r1 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func);
            var r2 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func);
            var r3 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func);

            r1.HasValue.ShouldBeTrue();
            r2.HasValue.ShouldBeTrue();
            r3.HasValue.ShouldBeTrue();

            r1.Value.ShouldBe(Result);
            r2.Value.ShouldBe(Result);
            r3.Value.ShouldBe(Result);

            func.Received(1).Invoke();
        }

        [Test]
        public void GetAndStore_NullValue_ShouldQueryForDataOnce()
        {
            var func = Substitute.For<Func<Maybe<string>>>();
            func.Invoke().Returns((Maybe<string>)null);

            var r1 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func);
            var r2 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func);
            var r3 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func);

            r1.HasNoValue.ShouldBeTrue();
            r2.HasNoValue.ShouldBeTrue();
            r3.HasNoValue.ShouldBeTrue();

            func.Received(1).Invoke();
        }

        [Test]
        public void GetAndStore_WithException_ShouldQueryForDataBeCalledTwice()
        {
            var func1 = Substitute.For<Func<Maybe<string>>>();
            func1.Invoke().Throws<Exception>();
            var func2 = Substitute.For<Func<Maybe<string>>>();
            func2.Invoke().Returns(Result);

            Action a = () => _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func1);
            a.ShouldThrow<Exception>();
            var r2 = _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, func2);

            r2.HasValue.ShouldBeTrue();

            r2.Value.ShouldBe(Result);

            func1.Received(1).Invoke();
            func2.Received(1).Invoke();
        }

        [Test]
        public async Task GetAndStore_ConcurrentCalls_ShouldQueryForDataBeCalledOnlyOnce()
        {
            var func = GetConfiguredFuncWithDelay(Result);

            var t1 = Task.Run(() => _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, () => func()));
            var t2 = Task.Run(() => _cacheProvider.GetAndStore((NonEmptyString)Key, _cacheTimeSpan, () => func()));
            var r1 = await t1.ConfigureAwait(false);
            var r2 = await t2.ConfigureAwait(false);

            r1.HasValue.ShouldBeTrue();
            r2.HasValue.ShouldBeTrue();

            r1.Value.ShouldBe(Result);
            r2.Value.ShouldBe(Result);

            func.Received(1).Invoke();
        }

        private static void ResetCache()
        {
            var objectCache = MemoryCache.Default;
            var keys = objectCache.Select(pair => pair.Key);
            foreach (var key in keys)
            {
                objectCache.Remove(key);
            }
        }

        private static Func<Maybe<T>> GetConfiguredFuncWithDelay<T>(T result)
            where T : class
        {
            var func = Substitute.For<Func<Maybe<T>>>();
            func.Invoke().Returns(
                info =>
                {
                    Thread.Sleep(2000);
                    return result;
                });
            return func;
        }
    }
}
