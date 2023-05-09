using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;

namespace Online_Shopping_System.IRepository
{
    public interface IProducts
    {
        public Task<MessageHelper> CreateProduct(List<ProductDTO> obj);

        public Task<ProductLandingPaginationDTO> GetProductLandingPagination( string viewOrder, long pageNo, long pageSize);
        public Task<ProductDTO> GetProduct(long productId);

    }
}
