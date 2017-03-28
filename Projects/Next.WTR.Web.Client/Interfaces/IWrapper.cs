namespace Next.WTR.Web.Client.Interfaces
{
    using System.Threading.Tasks;
    using Next.WTR.Common.Dtos;
    using Next.WTR.Web.Dtos.Apis.Account.Login;
    using Next.WTR.Web.Dtos.Apis.Product.FilterPaged;

    public interface IWrapper
    {
        Task<string> VersionGet();

        Task AccountLogout();

        Task AccountLogin(Data data);

        Task<Paged<Product>> ProductsFilterPaged(int skip, int top, string filter, string orderBy);

        Task<int> ProductsInsert(Dtos.Apis.Product.Insert.Product product);
    }
}