using System;
using System.Collections.Generic;

namespace EFCoreExpandTest.DomainModels
{
    public partial class Product
    {
        public Product()
        {
            ProductPrices = new HashSet<ProductPrice>();
        }

        public string Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}

