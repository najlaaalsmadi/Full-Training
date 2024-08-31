﻿namespace Backend.DTO
{
    public class ProductDTO { 
     public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? CategoryId { get; set; }

    public IFormFile? ProductImage { get; set; }


}
}
