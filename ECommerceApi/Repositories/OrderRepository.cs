using Microsoft.Data.SqlClient;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public Order Create(int userId, string shippingAddress, decimal totalPrice, List<OrderItem> items)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            // insert order
            using var orderCmd = new SqlCommand(
                @"INSERT INTO Orders (UserId, ShippingAddress, TotalPrice)
                  OUTPUT INSERTED.Id, INSERTED.CreatedAt
                  VALUES (@UserId, @ShippingAddress, @TotalPrice)", connection, transaction);

            orderCmd.Parameters.AddWithValue("@UserId", userId);
            orderCmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
            orderCmd.Parameters.AddWithValue("@TotalPrice", totalPrice);

            using var reader = orderCmd.ExecuteReader();
            reader.Read();
            var orderId = reader.GetInt32(0);
            var createdAt = reader.GetDateTime(1);
            reader.Close();

            // insert order items
            foreach (var item in items)
            {
                using var itemCmd = new SqlCommand(
                    @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
                      VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)", connection, transaction);

                itemCmd.Parameters.AddWithValue("@OrderId", orderId);
                itemCmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                itemCmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                itemCmd.ExecuteNonQuery();
            }

            transaction.Commit();

            return new Order
            {
                Id = orderId,
                UserId = userId,
                ShippingAddress = shippingAddress,
                TotalPrice = totalPrice,
                CreatedAt = createdAt
            };
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public List<Order> GetByUserId(int userId)
    {
        var orders = new List<Order>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            "SELECT Id, UserId, ShippingAddress, TotalPrice, CreatedAt FROM Orders WHERE UserId = @UserId ORDER BY CreatedAt DESC", connection);

        command.Parameters.AddWithValue("@UserId", userId);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            orders.Add(new Order
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                ShippingAddress = reader.GetString(2),
                TotalPrice = reader.GetDecimal(3),
                CreatedAt = reader.GetDateTime(4)
            });
        }

        return orders;
    }
}