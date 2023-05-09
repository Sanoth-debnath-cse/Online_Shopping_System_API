using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Online_Shopping_System.Models
{
    [Table("ProductTbl")]
    public partial class ProductTbl
    {
        [Key]
        public long ProductId { get; set; }
        [StringLength(200)]
        public string ProductName { get; set; } = null!;
        [Column(TypeName = "numeric(18, 4)")]
        public decimal ProductUnitPrice { get; set; }
        [StringLength(250)]
        public string ProductDescription { get; set; } = null!;
        [Column("isActive")]
        public bool IsActive { get; set; }
    }
}
