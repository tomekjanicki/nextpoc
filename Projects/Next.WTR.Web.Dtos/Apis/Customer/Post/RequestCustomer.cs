namespace Next.WTR.Web.Dtos.Apis.Customer.Post
{
    using Common.Dtos;

    public sealed class RequestCustomer
    {
        public RequestCustomer(string surname, string name, string phoneNumber, string address)
        {
            Surname = surname.IfNullReplaceWithEmptyString();
            Name = name.IfNullReplaceWithEmptyString();
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public string Surname { get; }

        public string Name { get;  }

        public string PhoneNumber { get; }

        public string Address { get; }
    }
}
