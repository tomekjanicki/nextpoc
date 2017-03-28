namespace Next.WTR.Web.Client.TestApp
{
    using System;
    using System.Threading.Tasks;
    using Next.WTR.Web.Dtos.Apis.Account.Login;
    using Next.WTR.Web.Dtos.Apis.Product.Insert;

    public class Program
    {
        public static void Main()
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            try
            {
                var wrapper = new Wrapper(new Uri(@"http://localhost:2776"));

                var version = await wrapper.VersionGet().ConfigureAwait(false);

                Console.WriteLine(version);

                await wrapper.AccountLogin(new Data("QC Buyer", "c2869901eb1b0ba90f30001f52191b8d")).ConfigureAwait(false);

                var result = await wrapper.ProductsFilterPaged(0, 10, string.Empty, string.Empty).ConfigureAwait(false);

                Console.WriteLine(result);

                var id = await wrapper.ProductsInsert(new Product(DateTime.Now.ToLongTimeString(), "n", 2m)).ConfigureAwait(false);

                Console.WriteLine(id);

                await wrapper.AccountLogout().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
