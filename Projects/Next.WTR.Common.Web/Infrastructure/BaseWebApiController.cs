namespace Next.WTR.Common.Web.Infrastructure
{
    using System.Security.Claims;
    using System.Web.Http;
    using Next.WTR.Common.Shared;
    using Next.WTR.Types.FunctionalExtensions;

    public abstract class BaseWebApiController : ApiController
    {
        public new ClaimsPrincipal User => base.User as ClaimsPrincipal;

        protected IHttpActionResult GetHttpActionResult<T>(IResult<T, Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResult(result, this);
        }

        protected IHttpActionResult GetHttpActionResult(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResult(result, this);
        }

        protected IHttpActionResult GetHttpActionResultForDelete(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResultForDelete(result, this);
        }

        protected IHttpActionResult GetHttpActionResultForUpdate(IResult<Error> result)
        {
            return WebApiControllerHelper.GetHttpActionResultForUpdate(result, this);
        }
    }
}