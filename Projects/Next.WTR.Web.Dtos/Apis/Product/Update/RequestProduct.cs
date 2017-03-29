namespace Next.WTR.Web.Dtos.Apis.Product.Update
{
    using Next.WTR.Common.Dtos;

    public class RequestProduct
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
