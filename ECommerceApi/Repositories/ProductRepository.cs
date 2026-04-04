using Microsoft.Data.SqlClient;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public List<Product> GetAll()
    {
        var products = new List<Product>();

        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT Id, Name, Description, Price, ImageUrl, Category, InStock FROM Products", connection);
        connection.Open();
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            products.Add(MapProduct(reader));
        }

        return products;
    }

    public Product? GetById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT Id, Name, Description, Price, ImageUrl, Category, InStock FROM Products WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();

        return reader.Read() ? MapProduct(reader) : null;
    }

    private Product MapProduct(SqlDataReader reader)
    {
        return new Product
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
            Price = reader.GetDecimal(3),
            ImageUrl = reader.IsDBNull(4) ? null : reader.GetString(4),
            Category = reader.IsDBNull(5) ? null : reader.GetString(5),
            InStock = reader.GetBoolean(6)
        };
    }
}