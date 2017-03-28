﻿namespace Next.WTR.Logic.Facades.Apis
{
    using AutoMapper;
    using Next.WTR.Common.Dtos;
    using Next.WTR.Common.Facades;
    using Next.WTR.Common.Handlers.Interfaces;
    using Next.WTR.Common.Shared;
    using Next.WTR.Logic.CQ.Product.FilterPaged;
    using Next.WTR.Types.FunctionalExtensions;

    public sealed class ProductsFilterPagedFacade
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductsFilterPagedFacade(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IResult<Paged<Web.Dtos.Apis.Product.FilterPaged.Product>, Error> FilterPaged(int skip, int top, string filter, string orderBy)
        {
            var queryResult = Query.TryCreate(orderBy, skip, top, filter);

            return Helper.GetItems<Paged<Web.Dtos.Apis.Product.FilterPaged.Product>, Query, Common.ValueObjects.Paged<Product>>(_mediator, _mapper, queryResult);
        }
    }
}