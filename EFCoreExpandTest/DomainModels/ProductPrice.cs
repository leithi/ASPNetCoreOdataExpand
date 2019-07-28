using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreExpandTest.DomainModels
{
    public partial class ProductPrice
    {
        public string VendorId { get; set; }
        public string ProductId { get; set; }
        public decimal Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
