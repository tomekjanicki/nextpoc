namespace Next.WTR.Web.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Next.WTR.Common.Dtos;
    using Next.WTR.Web.Client.Interfaces;
    using Next.WTR.Web.Dtos.Apis.Account.Login;
    using Next.WTR.Web.Dtos.Apis.Product.FilterPaged;

    public sealed class Wrapper : IWrapper
    {
        private const string CookieName = ".AspNet.ApplicationCookie";
        private readonly Uri _baseUri;
        private Tuple<string, string> _userIdWithPath;

        public Wrapper(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<string> VersionGet()
        {
            using (var client = GetHttpClient())
            {
                var httpResponseMessage = await client.GetAsync(GetFullUri(_baseUri, "version/get")).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
                var result = await httpResponseMessage.Content.ReadAsAsync<string>().ConfigureAwait(false);
                return result;
            }
        }

        public async Task AccountLogout()
        {
            using (var client = GetHttpClient())
            {
                var httpResponseMessage = await client.PostAsJsonAsync(GetFullUri(_baseUri, "account/logout"), string.Empty).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
                _userIdWithPath = null;
            }
        }

        public async Task AccountLogin(Data data)
        {
            _userIdWithPath = null;
            using (var client = GetHttpClient())
            {
                var httpResponseMessage = await client.PostAsJsonAsync(GetFullUri(_baseUri, "account/login"), data).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
                var cookies = httpResponseMessage.Headers.GetValues("Set-Cookie");
                _userIdWithPath = GetUserIdAndPath(cookies);
            }
        }

        public async Task<Paged<Product>> ProductsFilterPaged(int skip, int top, string filter, string orderBy)
        {
            using (var client = GetHttpClient())
            {
                var pars = new Dictionary<string, string>
                {
                    { "skip", skip.ToString() },
                    { "top", top.ToString() },
                    { "filter", filter },
                    { "orderBy", orderBy }
                };
                var httpResponseMessage = await client.GetAsync(GetFullUri(_baseUri, "products/filterpaged", pars)).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
                var result = await httpResponseMessage.Content.ReadAsAsync<Paged<Product>>().ConfigureAwait(false);
                return result;
            }
        }

        public async Task<int> ProductsInsert(Dtos.Apis.Product.Insert.Product product)
        {
            using (var client = GetHttpClient())
            {
                var httpResponseMessage = await client.PostAsJsonAsync(GetFullUri(_baseUri, "products/insert"), product).ConfigureAwait(false);
                httpResponseMessage.EnsureSuccessStatusCode();
                var result = await httpResponseMessage.Content.ReadAsAsync<int>().ConfigureAwait(false);
                return result;
            }
        }

        private static Uri GetFullUri(Uri baseUri, string relativeUri, Dictionary<string, string> parameters = null)
        {
            var parametersString = parameters == null || parameters.Count == 0 ? string.Empty : $"?{string.Join("&", parameters.Select(pair => $"{Uri.EscapeUriString(pair.Key)}={Uri.EscapeUriString(pair.Value)}"))}";

            return new Uri($"{baseUri}/{relativeUri}{parametersString}");
        }

        private static Tuple<string, string> GetUserIdAndPath(IEnumerable<string> cookies)
        {
            if (cookies == null)
            {
                throw new InvalidOperationException();
            }

            var cookiesArray = cookies.ToArray();
            if (cookiesArray.Length == 0)
            {
                throw new InvalidOperationException();
            }

            foreach (var cookie in cookiesArray)
            {
                var userId = string.Empty;
                var path = string.Empty;
                var cookieElements = cookie.Split(';').Select(s => s.Trim()).ToList();
                foreach (var cookieElement in cookieElements)
                {
                    if (cookieElement.StartsWith($"{CookieName}=", StringComparison.OrdinalIgnoreCase))
                    {
                        userId = GetValue(cookieElement);
                    }
                    else
                    {
                        if (cookieElement.StartsWith("path=", StringComparison.OrdinalIgnoreCase))
                        {
                            path = GetValue(cookieElement);
                        }
                    }

                    if (userId != string.Empty && path != string.Empty)
                    {
                        return new Tuple<string, string>(userId, path);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private static string GetValue(string cookieElement)
        {
            var keyWithValue = cookieElement.Split('=').ToArray();
            return keyWithValue.Length == 2 ? keyWithValue[1] : string.Empty;
        }

        private HttpClient GetHttpClient()
        {
            var cookieContainer = new CookieContainer();

            if (_userIdWithPath != null)
            {
                cookieContainer.Add(_baseUri, new Cookie(CookieName, _userIdWithPath.Item1, _userIdWithPath.Item2));
            }

            return new HttpClient(new HttpClientHandler { CookieContainer = cookieContainer });
        }
    }
}
