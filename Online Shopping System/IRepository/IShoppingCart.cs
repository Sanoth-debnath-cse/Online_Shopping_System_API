using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;

namespace Online_Shopping_System.IRepository
{
    public interface IShoppingCart
    {
        public Task<MessageHelper> AddToShoppingCart(List<ShoppingCartDTO> products);

        public Task<ShoppingCartCommonDTO> GetShoppingCart(long shoppingCartId);

        public Task<MessageHelper> EditShoppingCart(ShoppingCartCommonEditDTO objEdit);
    }
}
