namespace Next.WTR.Web.Dtos.Apis.Product.Insert
{
    public class Product
    {
        public Product(string code, string name, decimal? price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public string Code { get; }

        public string Name { get;  }

        public decimal? Price { get; }
    }
}
