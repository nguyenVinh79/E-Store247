namespace ShopBanHang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public ProductColorSize DetailProduct { get; set; }
        public int Quantity { get; set; }
    }
}