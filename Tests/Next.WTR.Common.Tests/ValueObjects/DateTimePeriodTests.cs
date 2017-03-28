namespace Next.WTR.Common.Tests.ValueObjects
{
    using System;
    using System.Collections;
    using Next.WTR.Common.Test;
    using Next.WTR.Common.ValueObjects;
    using NUnit.Framework;
    using Shouldly;

    public class DateTimePeriodTests
    {
        [Test]
        public void ValidDateTimeValues_ShouldSucceed()
        {
            var now = DateTime.Now;
            var result = DateTimePeriod.TryCreate(now, now);
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void ValidStringValues_ShouldSucceed()
        {
            var result = DateTimePeriod.TryCreate("2011-11-11 11:11:11", "2011-11-11 11:11:12");
            result.IsSuccess.ShouldBeTrue();
        }

        [Test]
        public void InValidDateTimeValues_ShouldFail()
        {
            var now = DateTime.Now;
            var result = DateTimePeriod.TryCreate(now, now.AddSeconds(-1));
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        [TestCaseSource(typeof(TestDataProvider), nameof(TestDataProvider.IncorrectData))]
        public void InValidStringValues_ShouldFail(string from, string to)
        {
            var result = DateTimePeriod.TryCreate(from, to);
            result.IsFailure.ShouldBeTrue();
        }

        [Test]
        public void TwoDateTimePeriodsWithSameValueShouldBeEqual()
        {
            var now = DateTime.Now;
            var r1 = DateTimePeriod.TryCreate(now, now.AddSeconds(1));
            var r2 = DateTimePeriod.TryCreate(now, now.AddSeconds(1));

            Helper.ShouldBeEqual(r1, r2);
        }

        [Test]
        public void TwoDateTimePeriodsWithDifferentValueShouldNotBeEqual()
        {
            var now = DateTime.Now;
            var r1 = DateTimePeriod.TryCreate(now, now);
            var r2 = DateTimePeriod.TryCreate(now, now.AddSeconds(1));

            Helper.ShouldNotBeEqual(r1, r2);
        }

        private class TestDataProvider
        {
            public static IEnumerable IncorrectData
            {
                get
                {
                    yield return new TestCaseData("2011-11-11 11:11:11", "2011-11-11 11:11:10");
                    yield return new TestCaseData("2011-32-11 11:11:11", "2011-11-11 11:11:10");
                    yield return new TestCaseData("2011-11-11 11:11:11", "2011-32-11 11:11:10");
                    yield return new TestCaseData(string.Empty, "2011-11-11 11:11:10");
                    yield return new TestCaseData("2011-11-11 11:11:11", string.Empty);
                }
            }
        }
    }
}
