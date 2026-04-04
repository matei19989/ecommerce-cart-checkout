using Microsoft.Data.SqlClient;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public User? GetByEmail(string email)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand("SELECT Id, Name, Email, PasswordHash, CreatedAt FROM Users WHERE Email = @Email", connection);
        command.Parameters.AddWithValue("@Email", email);
        connection.Open();
        using var reader = command.ExecuteReader();

        if (!reader.Read()) return null;

        return new User
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Email = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            CreatedAt = reader.GetDateTime(4)
        };
    }

    public User Create(string name, string email, string passwordHash)
    {
        using var connection = new SqlConnection(_connectionString);
        using var command = new SqlCommand(
            @"INSERT INTO Users (Name, Email, PasswordHash) 
              OUTPUT INSERTED.Id, INSERTED.CreatedAt 
              VALUES (@Name, @Email, @PasswordHash)", connection);

        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Email", email);
        command.Parameters.AddWithValue("@PasswordHash", passwordHash);
        connection.Open();
        using var reader = command.ExecuteReader();
        reader.Read();

        return new User
        {
            Id = reader.GetInt32(0),
            Name = name,
            Email = email,
            PasswordHash = passwordHash,
            CreatedAt = reader.GetDateTime(1)
        };
    }
}