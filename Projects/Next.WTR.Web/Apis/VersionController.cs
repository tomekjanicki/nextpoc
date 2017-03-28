namespace Next.WTR.Web.Apis
{
    using System.Web.Http;
    using Next.WTR.Common.Web.Infrastructure;
    using Next.WTR.Logic.Facades.Apis;

    public sealed class VersionController : BaseWebApiController
    {
        private readonly VersionGetFacade _versionGetFacade;

        public VersionController(VersionGetFacade versionGetFacade)
        {
            _versionGetFacade = versionGetFacade;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = _versionGetFacade.Get();

            return GetHttpActionResult(result);
        }
    }
}