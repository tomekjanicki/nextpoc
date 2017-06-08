namespace Next.WTR.Web.Dtos.Apis.Customer.Post
{
    using Common.Dtos;

    public sealed class RequestCustomer
    {
        public RequestCustomer(string code, string name, decimal? price)
        {
            Code = code.IfNullReplaceWithEmptyString();
            Name = name.IfNullReplaceWithEmptyString();
            Price = price;
        }

        public string Code { get; }

        public string Name { get;  }

        public decimal? Price { get; }
    }
}
