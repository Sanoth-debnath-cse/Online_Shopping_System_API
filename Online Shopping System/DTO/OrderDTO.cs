namespace Online_Shopping_System.DTO
{
    public class OrderDTO
    {
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalShoppingCost { get; set; }
    }
}
