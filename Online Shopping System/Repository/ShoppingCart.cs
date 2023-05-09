using Online_Shopping_System.DbContexts;
using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;
using Online_Shopping_System.IRepository;

namespace Online_Shopping_System.Repository
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly AppDbContext _context;

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> AddToShoppingCart(List<ShoppingCartDTO> products)
        {
            try
            {
                if (products.Count < 1)
                    throw new Exception("Add at least one items into cart");
                var headerCart = new Models.ShoppingCartHeader()
                {
                    IsCheckout = false,
                    ShoppingDatetime = DateTime.Now,
                    IsActive = true,
                    TotalShoppingCost = 0,
                };
                await _context.ShoppingCartHeaders.AddAsync(headerCart);
                await _context.SaveChangesAsync();
                var cartlist = new List<Models.ShoppingCartRow>();
                decimal totalShoppingValue = 0;
                foreach (var product in products)
                {
                    var itemCost = _context.ProductTbls.Where(p => p.ProductId == product.ProductId).Select(y => y.ProductUnitPrice).FirstOrDefault();
                    var totalProductValue = itemCost * product.ProductQuantity;

                    var rowCart = new Models.ShoppingCartRow()
                    {
                        CartId = headerCart.CartId,
                        ProductId = product.ProductId,
                        ProductQuantity = product.ProductQuantity,
                        TotalPrice = totalProductValue,
                    };

                    cartlist.Add(rowCart);
                    totalShoppingValue += totalProductValue;

                }
                await _context.ShoppingCartRows.AddRangeAsync(cartlist);
                await _context.SaveChangesAsync();

                headerCart.TotalShoppingCost = totalShoppingValue;
                _context.ShoppingCartHeaders.Update(headerCart);
                await _context.SaveChangesAsync();

                var msg = new MessageHelper();
                msg.Message = "Products Added into shopping cart successfully";
                msg.statuscode = 200;
                return msg;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ShoppingCartCommonDTO> GetShoppingCart(long shoppingCartId)
        {
            try
            {
                var headerCart = await Task.FromResult((from h in _context.ShoppingCartHeaders
                                                       where h.CartId==shoppingCartId
                                                       && h.IsActive == true
                                                       select new ShoppingCartHeaderDTO
                                                       {
                                                           IsCheckout=h.IsCheckout,
                                                           ShoppingDatetime=h.ShoppingDatetime,
                                                           TotalShoppingCost=h.TotalShoppingCost,
                                                       }).FirstOrDefault());
                var rowCart = await Task.FromResult((from r in _context.ShoppingCartRows
                                                     join p in _context.ProductTbls on r.ProductId equals p.ProductId
                                                     where r.CartId == shoppingCartId
                                                     select new ShoppingCartRowDTO
                                                     {
                                                         ProductId = r.ProductId,
                                                         ProductName = p.ProductName,
                                                         ProductQuantity = r.ProductQuantity,
                                                         TotalPrice = r.TotalPrice,
                                                         RowId=r.RowId,
                                                     }).ToList());

                return new ShoppingCartCommonDTO()
                {
                    headeData = headerCart,
                    rowData = rowCart,
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MessageHelper> EditShoppingCart(ShoppingCartCommonEditDTO objEdit)
        {
            try
            {
                var cartData = _context.ShoppingCartHeaders.Where(x => x.CartId == objEdit.CartId && x.IsCheckout == false
                                                                  && x.IsActive == true).FirstOrDefault();
                if (cartData == null)
                    throw new Exception("Shopping Cart Is Empty");

                var updaterowList = new List<Models.ShoppingCartRow>();
                foreach (var edit in objEdit.rowdata)
                {
                    var rowdata = _context.ShoppingCartRows.Where(x => x.CartId == edit.CartId && x.RowId == edit.RowId).FirstOrDefault();
                    var itemCost = _context.ProductTbls.Where(p => p.ProductId == edit.ProductId).Select(y => y.ProductUnitPrice).FirstOrDefault();
                    var totalProductValue = itemCost * edit.ProductQuantity;

                    rowdata.ProductId=edit.ProductId;
                    rowdata.ProductQuantity = edit.ProductQuantity;
                    rowdata.TotalPrice = totalProductValue;

                    updaterowList.Add(rowdata);


                }
                _context.ShoppingCartRows.UpdateRange(updaterowList);
                await _context.SaveChangesAsync();

                var totalShoppingValue = _context.ShoppingCartRows.Where(x => x.CartId == objEdit.CartId).Select(y => y.TotalPrice).Sum();


                cartData.TotalShoppingCost = totalShoppingValue;
                cartData.ShoppingDatetime = DateTime.Now;
                _context.ShoppingCartHeaders.Update(cartData);
                await _context.SaveChangesAsync();

                var msg = new MessageHelper();
                msg.Message = "Shopping Cart Updated Successfully";
                msg.statuscode = 200;
                return msg;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
