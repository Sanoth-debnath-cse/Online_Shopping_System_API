using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Online_Shopping_System.Models
{
    [Table("ShoppingCartHeader")]
    public partial class ShoppingCartHeader
    {
        [Key]
        public long CartId { get; set; }
        [Column(TypeName = "numeric(18, 4)")]
        public decimal TotalShoppingCost { get; set; }
        [Column("isCheckout")]
        public bool IsCheckout { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ShoppingDatetime { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
    }
}
