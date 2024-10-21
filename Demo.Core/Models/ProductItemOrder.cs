namespace Demo.Core.Models
{
    public class ProductItemOrder
    {
        public ProductItemOrder() { }   
        public ProductItemOrder(int id, string productName, string pictureUrl)
        {
            Id = id;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int Id { get; set; } 
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}