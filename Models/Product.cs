namespace ForApplication.Models;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public string? Category { get; set; }
    public int? StockQuantity { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}