namespace Next.WTR.Logic.CQ.Product.Get
{
    using System;
    using Types;
    using Types.FunctionalExtensions;
    using ValueObjects;

    public class Product
    {
        public Product(PositiveInt id, Code code, Name name, NonNegativeDecimal price, DateTime? date, bool canDelete, byte[] versionInt)
            : this()
        {
            Id = id;
            Code = code;
            Name = name;
            Price = price;
            Date = date;
            CanDelete = canDelete;
            VersionInt = versionInt;
            NonEmptyString.TryCreate(Convert.ToBase64String(versionInt), (NonEmptyString)"Value").EnsureIsNotFaliure();
        }

        private Product()
        {
        }

        public PositiveInt Id { get; }

        public Code Code { get; }

        public Name Name { get; }

        public NonNegativeDecimal Price { get; }

        public DateTime? Date { get; }

        public bool CanDelete { get; }

        public NonEmptyString Version => (NonEmptyString)Convert.ToBase64String(VersionInt);

        private byte[] VersionInt { get; }
    }
}
