using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_System.DTO
{
    public class ShoppingCartDTO
    {
        public long ProductId { get; set; }
        public decimal ProductQuantity { get; set; }
    }

    public class ShoppingCartHeaderDTO
    {
        public decimal TotalShoppingCost { get; set; }
        public bool IsCheckout { get; set; }
        public DateTime ShoppingDatetime { get; set; }
    }

    public class ShoppingCartRowDTO : ShoppingCartDTO
    {
        public long RowId { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductName { get; set; }
    }

    public class ShoppingCartCommonDTO
    {
        public ShoppingCartHeaderDTO headeData { get; set; }
        public List<ShoppingCartRowDTO> rowData { get; set; }
    }

    public class EditShoppingCartDTO : ShoppingCartDTO
    {
        public long CartId { get; set; }
        public long RowId { get; set; }
    }

    public class ShoppingCartCommonEditDTO
    {
        public long CartId { get; set; }
        public List<EditShoppingCartDTO> rowdata { get; set; }
    }
}
