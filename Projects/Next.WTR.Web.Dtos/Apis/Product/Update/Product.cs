namespace Next.WTR.Web.Dtos.Apis.Product.Update
{
    public class Product
    {
        public Product(string name, decimal? price, string version)
        {
            Name = name;
            Price = price;
            Version = version;
        }

        public string Name { get; }

        public decimal? Price { get; }

        public string Version { get; }
    }
}
