namespace Next.WTR.Logic.Tests.Mappings
{
    using AutoMapper;
    using Common.Mappings;
    using Logic.CQ.Customer.Get;
    using Logic.CQ.Customer.ValueObjects;
    using Logic.Mappings;
    using NUnit.Framework;
    using Shouldly;
    using Types;
    using Web.Dtos.Apis.Customer.Get;

    public class MappingTests
    {
        private IMapper _mapper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mapper = Helper.GetMapper(AutoMapperConfiguration.Configure);
        }

        [Test]
        public void Customer_Should_Map_To_CustomerDto()
        {
            const int id = 1;
            const string code = "code";
            const string name = "name";
            const decimal price = 5.22M;
            var source = new Customer((PositiveInt)id, (Code)code, (Name)name, (NonNegativeDecimal)price, null, true, new byte[] { 1 });
            var result = _mapper.Map<ResponseCustomer>(source);
            result.Id.ShouldBe(id);
            result.Code.ShouldBe(code);
            result.Name.ShouldBe(name);
            result.Price.ShouldBe(price);
            result.Version.ShouldNotBeNullOrEmpty();
        }
    }
}
