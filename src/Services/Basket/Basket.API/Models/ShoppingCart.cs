namespace Basket.API.Models
{
	public class ShoppingCart
	{
		public string UserName { get; set; } = default!;
		public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
		public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

		// requried for mapping
        public ShoppingCart()
        {
            
        }

		public ShoppingCart(string userName)
		{
			UserName = userName;
		}
	}
}
