using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Online_Shopping_System.Models
{
    [Table("ShoppingCartRow")]
    public partial class ShoppingCartRow
    {
        [Key]
        public long RowId { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal ProductQuantity { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal TotalPrice { get; set; }
    }
}
