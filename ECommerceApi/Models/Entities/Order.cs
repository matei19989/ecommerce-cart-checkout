namespace ECommerceApi.Models.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}