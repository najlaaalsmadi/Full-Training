namespace Backend.DTO
{
    public class CartItemsResponseDTO
    {
        public int CartItemId { get; set; }

        public int? CartId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public ProductRequestDTO1 productRequestDTO { get; set; }
    }
    public class ProductRequestDTO1
    {
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }

        public IFormFile ProductImage { get; set; }


    }
}

