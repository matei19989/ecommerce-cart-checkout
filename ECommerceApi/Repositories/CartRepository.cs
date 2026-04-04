using Microsoft.Data.SqlClient;
using ECommerceApi.Models.DTOs;

namespace ECommerceApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly string _connectionString;

    public CartRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public List<CartItemResponse> GetByUserId(int userId)
    {
        var items = new List<CartItemResponse>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            @"SELECT ci.Id, ci.ProductId, p.Name, p.Price, ci.Quantity, p.ImageUrl
              FROM CartItems ci
              JOIN Products p ON ci.ProductId = p.Id
              WHERE ci.UserId = @UserId", connection);

        command.Parameters.AddWithValue("@UserId", userId);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            items.Add(new CartItemResponse
            {
                Id = reader.GetInt32(0),
                ProductId = reader.GetInt32(1),
                ProductName = reader.GetString(2),
                Price = reader.GetDecimal(3),
                Quantity = reader.GetInt32(4),
                ImageUrl = reader.IsDBNull(5) ? null : reader.GetString(5)
            });
        }

        return items;
    }

    public void AddItem(int userId, int productId, int quantity)
    {
        using var connection = new SqlConnection(_connectionString);
        // If item already exists, update quantity instead
        using var command = new SqlCommand(
            @"IF EXISTS (SELECT 1 FROM CartItems WHERE UserId = @UserId AND ProductId = @ProductId)
                UPDATE CartItems SET Quantity = Quantity + @Quantity WHERE UserId = @UserId AND ProductId = @ProductId
              ELSE
                INSERT INTO CartItems (UserId, ProductId, Quantity) VALUES (@UserId, @ProductId, @Quantity)", connection);

        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@ProductId", productId);
        command.Parameters.AddWithValue("@Quantity", quantity);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void UpdateQuantity(int userId, int productId, int quantity)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            "UPDATE CartItems SET Quantity = @Quantity WHERE UserId = @UserId AND ProductId = @ProductId", connection);

        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@ProductId", productId);
        command.Parameters.AddWithValue("@Quantity", quantity);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void RemoveItem(int userId, int productId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            "DELETE FROM CartItems WHERE UserId = @UserId AND ProductId = @ProductId", connection);

        command.Parameters.AddWithValue("@UserId", userId);
        command.Parameters.AddWithValue("@ProductId", productId);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void ClearCart(int userId)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("DELETE FROM CartItems WHERE UserId = @UserId", connection);
        command.Parameters.AddWithValue("@UserId", userId);
        connection.Open();
        command.ExecuteNonQuery();
    }
}