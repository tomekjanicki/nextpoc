namespace Next.WTR.Web.Dtos.Apis.Customer.Put
{
    using Common.Dtos;

    public sealed class RequestCustomer
    {
        public RequestCustomer(string name, decimal? price, string version)
        {
            Name = name.IfNullReplaceWithEmptyString();
            Price = price;
            Version = version.IfNullReplaceWithEmptyString();
        }

        public string Name { get; }

        public decimal? Price { get; }

        public string Version { get; }
    }
}
