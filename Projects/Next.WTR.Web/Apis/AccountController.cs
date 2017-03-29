namespace Next.WTR.Web.Apis
{
    using System.Linq;
    using System.Web.Http;
    using Next.WTR.Common.Infrastructure.Security;
    using Next.WTR.Common.Web.Infrastructure;
    using Next.WTR.Logic.Facades.Apis;
    using Next.WTR.Web.Dtos.Apis.Account.Login;

    public sealed class AccountController : BaseWebApiController
    {
        private readonly AccountLoginFacade _accountLoginFacade;
        private readonly AccountLogoutFacade _accountLogoutFacade;

        public AccountController(AccountLoginFacade accountLoginFacade, AccountLogoutFacade accountLogoutFacade)
        {
            _accountLoginFacade = accountLoginFacade;
            _accountLogoutFacade = accountLogoutFacade;
        }

        [HttpPost]
        public IHttpActionResult Login(Data data)
        {
            var result = _accountLoginFacade.Login(data);
            return GetHttpActionResult(result);
        }

        [HttpPost]
        public IHttpActionResult Logout()
        {
            var sessionId = User.Claims.First(claim => claim.Type == ClaimTypes.SessionId).Value;
            var result = _accountLogoutFacade.Logout(sessionId);
            return GetHttpActionResult(result);
        }
    }
}