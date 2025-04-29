namespace Domain.Models.OrderModels
{
    public class ProductInOrderItem
    {
        public ProductInOrderItem()
        {
            
        }

        public ProductInOrderItem(int productId, string productName, string pictureUrl)
        {

            productId = productId;
            productName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}