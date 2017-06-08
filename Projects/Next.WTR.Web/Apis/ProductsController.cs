namespace Next.WTR.Web.Apis
{
    using System.Web.Http;
    using Common.Web.Infrastructure;
    using Logic.Facades.Apis;
    using Types;

    public sealed class ProductsController : BaseWebApiController
    {
        private readonly ProductsFilterPagedFacade _productsFilterPagedFacade;
        private readonly ProductsGetFacade _productsGetFacade;
        private readonly ProductsDeleteFacade _productsDeleteFacade;
        private readonly ProductsUpdateFacade _productsUpdateFacade;
        private readonly ProductsInsertFacade _productsInsertFacade;

        public ProductsController(ProductsFilterPagedFacade productsFilterPagedFacade, ProductsGetFacade productsGetFacade, ProductsDeleteFacade productsDeleteFacade, ProductsUpdateFacade productsUpdateFacade, ProductsInsertFacade productsInsertFacade)
        {
            _productsFilterPagedFacade = productsFilterPagedFacade;
            _productsGetFacade = productsGetFacade;
            _productsDeleteFacade = productsDeleteFacade;
            _productsUpdateFacade = productsUpdateFacade;
            _productsInsertFacade = productsInsertFacade;
        }

        [HttpGet]
        public IHttpActionResult FilterPaged(int skip, int top, string filter = null, string orderBy = null)
        {
            var result = _productsFilterPagedFacade.FilterPaged(skip, top, filter.IfNullReplaceWithEmptyString(), orderBy.IfNullReplaceWithEmptyString());

            return GetHttpActionResult(result);
        }

        [HttpPost]
        public IHttpActionResult Update(int id, Dtos.Apis.Product.Update.RequestProduct requestProduct)
        {
            var result = _productsUpdateFacade.Update(id, requestProduct);

            return GetHttpActionResultForUpdate(result);
        }

        [HttpPost]
        public IHttpActionResult Insert(Dtos.Apis.Product.Insert.RequestProduct requestProduct)
        {
            var result = _productsInsertFacade.Insert(requestProduct);

            return GetHttpActionResult(result);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = _productsGetFacade.Get(id);

            return GetHttpActionResult(result);
        }

        [HttpPost]
        public IHttpActionResult Delete(int id, string version)
        {
            var result = _productsDeleteFacade.Delete(id, version.IfNullReplaceWithEmptyString());

            return GetHttpActionResultForDelete(result);
        }
    }
}
