using Online_Shopping_System.DbContexts;
using Online_Shopping_System.DTO;
using Online_Shopping_System.Helper;
using Online_Shopping_System.IRepository;
using System.Linq;

namespace Online_Shopping_System.Repository
{
    public class Products : IProducts
    {
        private readonly AppDbContext _context;

        public Products(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MessageHelper> CreateProduct(List<ProductDTO> obj)
        {
            try
            {
                if (obj.Count < 1)
                {
                    throw new Exception("Add at least one Item");
                }
                var itemList = new List<Models.ProductTbl>();
                foreach(var item in obj)
                {
                    var product = new Models.ProductTbl
                    {
                        ProductName= item.ProductName,
                        ProductDescription= item.ProductDescription,
                        ProductUnitPrice= item.ProductUnitPrice,
                        IsActive=true,
                    };
                    itemList.Add(product);
                }
                await _context.ProductTbls.AddRangeAsync(itemList);
                await _context.SaveChangesAsync();

                var msg = new MessageHelper();
                msg.Message = "Product create successfully";
                msg.statuscode = 200;
                return msg;
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductLandingPaginationDTO> GetProductLandingPagination( string viewOrder, long pageNo, long pageSize)
        {
            try
            {
                var products = await Task.FromResult(from p in _context.ProductTbls
                                                      where p.IsActive == true
                                                      select new ProductLandingDTO
                                                      {
                                                          ProductName = p.ProductName,
                                                          ProductDescription = p.ProductDescription,
                                                          ProductUnitPrice = p.ProductUnitPrice,

                                                      });

                if (viewOrder.ToUpper() == "ASC")
                    products = products.OrderBy(o => o.ProductUnitPrice);
                else if(viewOrder.ToUpper()=="DESC")
                    products=products.OrderByDescending(o => o.ProductUnitPrice);
                if(pageNo<1)
                    pageNo= 1;
                var itemdata = PagingList<ProductLandingDTO>.CreateAsync(products, pageNo, pageSize);
                int index = 1;
                foreach(var item in itemdata)
                {
                    item.SL=index++;
                }
                return new ProductLandingPaginationDTO
                {
                    Productls=itemdata,
                    currentPage=pageNo,
                    pageSize=pageSize,
                    totalCount=products.Count()
                    
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductDTO> GetProduct(long productId)
        {
            try
            {
                var product = ((from p in _context.ProductTbls
                               where p.ProductId == productId
                               && p.IsActive == true
                               select new ProductDTO
                                {
                                             ProductName = p.ProductName,
                                             ProductUnitPrice = p.ProductUnitPrice,
                                             ProductDescription = p.ProductDescription,
                                }).FirstOrDefault());
                return product;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
