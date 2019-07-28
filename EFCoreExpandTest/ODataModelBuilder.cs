

using System;
using EFCoreExpandTest.DomainModels;
using EFCoreExpandTest.DTOModels;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace EFCoreExpandTest.OData
{
    public class ODataModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);
            //product
            builder.EntitySet<ProductDTO>("Products").EntityType.Select().Filter().OrderBy().Expand().Count()
                .Page(100, 20);
            //productprice
            builder.EntitySet<ProductPriceDTO>("ProductPrices").EntityType.Select().Filter().OrderBy().Expand().Count().Page();
            //vendor
            builder.EntitySet<VendorDTO>("Vendors").EntityType.Select().Filter().OrderBy().Expand().Count().Page();
            return builder.GetEdmModel();
        }
    }
}
