namespace Next.WTR.Web.Dtos.Apis.Product.FilterPaged
{
    using System;

    public class Product
    {
        public Product(int id, string code, string name, decimal price, DateTime? date, bool canDelete, string version)
        {
            Id = id;
            Code = code;
            Name = name;
            Price = price;
            Date = date;
            CanDelete = canDelete;
            Version = version;
        }

        public int Id { get; }

        public string Code { get; }

        public string Name { get; }

        public decimal Price { get; }

        public DateTime? Date { get; }

        public bool CanDelete { get; }

        public string Version { get; }
    }
}
