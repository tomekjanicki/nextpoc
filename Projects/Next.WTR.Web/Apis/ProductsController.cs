namespace Next.WTR.Web.Apis
{
    using System.Web.Http;
    using Common.Web.Infrastructure;
    using Dtos.Apis.Product.Post;
    using Logic.Facades.Apis;
    using Types;

    public sealed class ProductsController : BaseWebApiController
    {
        private readonly ProductsFilterPagedFacade _productsFilterPagedFacade;
        private readonly ProductsGetFacade _productsGetFacade;
        private readonly ProductsDeleteFacade _productsDeleteFacade;
        private readonly ProductsPutFacade _productsPutFacade;
        private readonly ProductsPostFacade _productsPostFacade;

        public ProductsController(ProductsFilterPagedFacade productsFilterPagedFacade, ProductsGetFacade productsGetFacade, ProductsDeleteFacade productsDeleteFacade, ProductsPutFacade productsPutFacade, ProductsPostFacade productsPostFacade)
        {
            _productsFilterPagedFacade = productsFilterPagedFacade;
            _productsGetFacade = productsGetFacade;
            _productsDeleteFacade = productsDeleteFacade;
            _productsPutFacade = productsPutFacade;
            _productsPostFacade = productsPostFacade;
        }

        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var result = _productsFilterPagedFacade.FilterPaged(skip, top, filter.IfNullReplaceWithEmptyString(), orderBy.IfNullReplaceWithEmptyString());

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Put(int id, Dtos.Apis.Product.Put.RequestProduct requestProduct)
        {
            var result = _productsPutFacade.Put(id, requestProduct);

            return GetHttpActionResultForPut(result);
        }

        public IHttpActionResult Post(RequestProduct requestProduct)
        {
            var result = _productsPostFacade.Post(requestProduct);

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Get(int id)
        {
            var result = _productsGetFacade.Get(id);

            return GetHttpActionResult(result);
        }

        public IHttpActionResult Delete(int id, string version)
        {
            var result = _productsDeleteFacade.Delete(id, version.IfNullReplaceWithEmptyString());

            return GetHttpActionResultForDelete(result);
        }
    }
}
