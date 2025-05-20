using CaseFlow.BLL.Interfaces.Shared;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace CaseFlow.BLL.Services;

public class PostgresUserService(IConfiguration config) : IPostgresUserService
{
    public async Task CreateUserAsync(string username, string password, string role)
    {
        var sql = $@"
            DO $$
            BEGIN
                IF NOT EXISTS (SELECT FROM pg_roles WHERE rolname = '{username}') THEN
                    CREATE ROLE ""{username}"" LOGIN PASSWORD '{password}';
                    GRANT ""{role}"" TO ""{username}"";
                END IF;
            END
            $$;
        ";

        await using var conn = new NpgsqlConnection(config.GetConnectionString("DetectiveAgencyDb"));
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateUsernameAsync(string oldUsername, string newUsername)
    {
        var sql = $@"ALTER ROLE ""{oldUsername}"" RENAME TO ""{newUsername}"";";

        await using var conn = new NpgsqlConnection(config.GetConnectionString("DetectiveAgencyDb"));
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync();
    }


    public async Task UpdatePasswordAsync(string username, string newPassword)
    {
        var sql = $@"ALTER ROLE ""{username}"" WITH PASSWORD @password;";

        await using var conn = new NpgsqlConnection(config.GetConnectionString("DetectiveAgencyDb"));
        await conn.OpenAsync();

        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("password", newPassword);

        await cmd.ExecuteNonQueryAsync();
    }


    public async Task DisableUserAsync(string username)
    {
        var sql = $@"ALTER ROLE ""{username}"" NOLOGIN;";
        
        await using var conn = new NpgsqlConnection(config.GetConnectionString("DetectiveAgencyDb"));
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task EnableUserAsync(string username)
    {
        var sql = $@"ALTER ROLE ""{username}"" LOGIN;";
        
        await using var conn = new NpgsqlConnection(config.GetConnectionString("DetectiveAgencyDb"));
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteUserAsync(string username)
    {
        var sql = $@"
            DO $$
            BEGIN
                IF EXISTS (SELECT FROM pg_roles WHERE rolname = '{username}') THEN
                    REASSIGN OWNED BY ""{username}"" TO CURRENT_USER;
                    DROP OWNED BY ""{username}"";
                    DROP ROLE ""{username}"";
                END IF;
            END
            $$;
        ";

        await using var conn = new NpgsqlConnection(config.GetConnectionString("DetectiveAgencyDb"));
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await cmd.ExecuteNonQueryAsync();
    }
}
