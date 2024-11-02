using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM Users WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var sql = "SELECT * FROM Users";
        return await _dbConnection.QueryAsync<User>(sql);
    }

    public async Task AddAsync(User user)
    {
        var sql = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
        await _dbConnection.ExecuteAsync(sql, user);
    }

    public async Task UpdateAsync(User user)
    {
        var sql = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(sql, user);
    }

    public async Task DeleteAsync(int id)
    {
        var sql = "DELETE FROM Users WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(sql, new { Id = id });
    }
}
