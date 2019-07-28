using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCoreExpandTest.DTOModels
{
    public class ProductDTO
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal AveragePrice { get; set; }
        public virtual ICollection<ProductPriceDTO> ProductPrices { get; set; }
    }
}
