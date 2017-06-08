namespace Next.WTR.Web.Dtos.Apis.Product.Insert
{
    using Common.Dtos;

    public class RequestProduct
    {
        public RequestProduct(string code, string name, decimal? price)
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
