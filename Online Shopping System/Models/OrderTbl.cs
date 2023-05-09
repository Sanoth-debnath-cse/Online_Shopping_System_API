using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Online_Shopping_System.Models
{
    [Table("OrderTbl")]
    public partial class OrderTbl
    {
        [Key]
        public long OrderId { get; set; }
        public long CartId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime OrderDatetime { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
    }
}
