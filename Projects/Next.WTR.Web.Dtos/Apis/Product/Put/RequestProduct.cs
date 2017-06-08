namespace Next.WTR.Web.Dtos.Apis.Product.Put
{
    using Common.Dtos;

    public sealed class RequestProduct
    {
        public RequestProduct(string name, decimal? price, string version)
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
