using System;
using System.Collections.Generic;

namespace EFCoreExpandTest.DomainModels
{
    public partial class Vendor
    {
        public Vendor()
        {
            ProductPrices = new HashSet<ProductPrice>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
