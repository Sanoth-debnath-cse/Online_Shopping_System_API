using Online_Shopping_System.DbContexts;
using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;
using Online_Shopping_System.IRepository;

namespace Online_Shopping_System.Repository
{
    public class Order : IOrder
    {
        private readonly AppDbContext _context;

        public Order(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CheckOut(long shoppingCartId)
        {
            try
            {
                var data = _context.ShoppingCartHeaders.Where(x => x.CartId == shoppingCartId && x.IsActive == true).FirstOrDefault();

                if (data == null)
                    throw new Exception("Shopping Cart not Available");
                var orderDate = new Models.OrderTbl()
                {
                    CartId = shoppingCartId,
                    OrderDatetime = DateTime.Now,
                    IsActive = true,
                };

                await _context.OrderTbls.AddAsync(orderDate);
                await _context.SaveChangesAsync();

                data.IsCheckout= true;
                _context.ShoppingCartHeaders.Update(data);
                await _context.SaveChangesAsync();

                var msg = new MessageHelper();
                msg.Message = "CheckOut successfully";
                msg.statuscode = 200;
                return msg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<OrderDTO>> OrderDetails(long OrderId)
        {
            try
            {
                var orderData = await Task.FromResult((from o in _context.OrderTbls
                                                       join r in _context.ShoppingCartRows on o.CartId equals r.CartId
                                                       join h in _context.ShoppingCartHeaders on r.CartId equals h.CartId
                                                       join p in _context.ProductTbls on r.ProductId equals p.ProductId
                                                       where o.OrderId == OrderId
                                                       && o.IsActive == true
                                                       select new OrderDTO
                                                       {
                                                           CartId = o.CartId,
                                                           ProductId = r.ProductId,
                                                           ProductName = p.ProductName,
                                                           ProductQuantity = r.ProductQuantity,
                                                           TotalPrice = r.TotalPrice,
                                                           TotalShoppingCost = h.TotalShoppingCost,
                                                       }).ToList());
                return orderData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
