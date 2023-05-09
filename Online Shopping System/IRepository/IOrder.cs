using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;

namespace Online_Shopping_System.IRepository
{
    public interface IOrder
    {
        public Task<MessageHelper> CheckOut(long shoppingCartId);

        public Task<List<OrderDTO>> OrderDetails(long OrderId);
    }
}
