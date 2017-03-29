﻿namespace Next.WTR.Web.Dtos.Apis.Product.Get
{
    using System;

    public class ResponseProduct
    {
        public ResponseProduct(int id, string code, string name, decimal price, DateTime? date, bool canDelete, string version)
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