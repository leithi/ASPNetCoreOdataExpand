using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreExpandTest.DTOModels
{
    public class ProductPriceDTO
    {
        [Key]
        [ForeignKey("Vendor")]
        public string VendorId { get; set; }
        [Key]
        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public decimal Price { get; set; }

        public virtual VendorDTO Vendor { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
