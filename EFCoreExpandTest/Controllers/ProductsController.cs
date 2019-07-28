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
    [ODataRoutePrefix("Products")]
    public class ProductsController : ODataController
    {
        protected readonly IMapper Mapper;
        private readonly TestContext Context;

        public ProductsController(TestContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ProductDTO> Get(ODataQueryOptions queryOptions)
        {
            var query = Context.Products;
            string[] includes = GetExpandNamesFromODataQuery(queryOptions);
            if (includes != null && includes.Length > 0)
            {
                return query.ProjectTo<ProductDTO>(null, includes);
            }
            return query.ProjectTo<ProductDTO>();
        }

        [EnableQuery]
        [ODataRoute("({key})")]
        public IQueryable<ProductDTO> Get([FromODataUri] string key, ODataQueryOptions queryOptions)
        {
            var query = Context.Products.Where(x => x.Id.Equals(key));
            string[] includes = GetExpandNamesFromODataQuery(queryOptions);
            if (includes != null && includes.Length > 0)
            {
                return query.ProjectTo<ProductDTO>(null, includes);
            }
            return query.ProjectTo<ProductDTO>();
        }

        /*[EnableQuery]
        [ODataRoute("({key})/Prices")]
        public IQueryable<ProductPriceDTO> GetPrices([FromODataUri] string key, ODataQueryOptions queryOptions)
        {
            var query = Context.ProductPrices.Where(x => x.ProductId.Equals(key));
            string[] includes = GetExpandNamesFromODataQuery(queryOptions);
            if (includes != null && includes.Length > 0)
            {
                return query.ProjectTo<ProductPriceDTO>(null, includes);
            }
            return query.ProjectTo<ProductPriceDTO>();
        }*/

        private ProductDTO MapProductToProductDTO(Product product)
        {
            ProductDTO pDTO = Mapper.Map<Product, ProductDTO>(product);
            if (product.ProductPrices != null)
            {
                pDTO.LowestPrice = product.ProductPrices.Min(x => x.Price);
                pDTO.HighestPrice = product.ProductPrices.Max(x => x.Price);
                pDTO.AveragePrice = product.ProductPrices.Average(x => x.Price);
            }
            return pDTO;
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