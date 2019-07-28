using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreExpandTest.DomainModels;
using EFCoreExpandTest.DTOModels;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreExpandTest.Controllers
{
    [ODataRoutePrefix("ProductPrices")]
    public class ProductPricesController : ODataController
    {
        protected readonly IMapper Mapper;
        private readonly TestContext Context;

        public ProductPricesController(TestContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ProductPriceDTO> Get(ODataQueryOptions queryOptions)
        {
            var query = Context.ProductPrices;
            string[] includes = GetExpandNamesFromODataQuery(queryOptions);
            if (includes != null && includes.Length > 0)
            {
                return query.ProjectTo<ProductPriceDTO>(null, includes);
            }
            return query.ProjectTo<ProductPriceDTO>();
        }
       
        protected string[] GetExpandNamesFromODataQuery(ODataQueryOptions queryOptions)
        {
            string[] includes = null;
            string includeText = queryOptions.SelectExpand != null ? queryOptions.SelectExpand.RawExpand : null;
            if (!string.IsNullOrEmpty(includeText))
            {
                includes = queryOptions.SelectExpand.RawExpand.Split(',');
            }
            return includes;
        }

    }
}