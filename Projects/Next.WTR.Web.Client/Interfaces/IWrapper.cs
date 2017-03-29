namespace Next.WTR.Web.Client.Interfaces
{
    using System.Threading.Tasks;
    using Next.WTR.Common.Dtos;
    using Next.WTR.Web.Dtos.Apis.Account.Login;
    using Next.WTR.Web.Dtos.Apis.Product.FilterPaged;
    using Next.WTR.Web.Dtos.Apis.Product.Insert;

    public interface IWrapper
    {
        Task<string> VersionGet();

        Task AccountLogout();

        Task AccountLogin(RequestUserIdAndPassword requestUserIdAndPassword);

        Task<Paged<ResponseProduct>> ProductsFilterPaged(int skip, int top, string filter, string orderBy);

        Task<int> ProductsInsert(RequestProduct requestProduct);
    }
}