using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Shopping_System.DTO
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public decimal ProductUnitPrice { get; set; }
        public string ProductDescription { get; set; }
    }

    public class ProductLandingPaginationDTO
    {
        public List<ProductLandingDTO> Productls { get; set; }
        public long currentPage { get; set; }
        public long totalCount { get; set; }
        public long pageSize { get; set; }

    }

    public class ProductLandingDTO : ProductDTO
    {
        public long SL { get; set; }
    }
}
