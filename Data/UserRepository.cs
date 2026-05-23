using MySqlConnector;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task CreateUserAsync(string username, string email)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            INSERT INTO Users (Username, Email)
            VALUES (@Username, @Email);
        ";

        await using var command = new MySqlCommand(sql, connection);

        command.Parameters.AddWithValue("@Username", username);
        command.Parameters.AddWithValue("@Email", email);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        const string sql = @"
            SELECT UserID, Username, Email
            FROM Users
            WHERE Username = @Username;
        ";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Username", username);

        await using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new User
        {
            UserID = reader.GetInt32("UserID"),
            Username = reader.GetString("Username"),
            Email = reader.GetString("Email")
        };
    }
}