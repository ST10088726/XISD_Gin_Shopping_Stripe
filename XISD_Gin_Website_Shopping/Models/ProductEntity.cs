namespace XISD_Gin_Website_Shopping.Models
{
    public class ProductEntity
    {
        public int Id {  get; set; }
        public string Product { get; set; }
        public long Price { get; set; }
        public long Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}
