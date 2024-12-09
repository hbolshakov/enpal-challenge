using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Enpal.HomeChallenge.Infrastructure.Repositories;

public class BaseRepository
{
    private readonly string _connectionString;
    protected IDbConnection Connection => new NpgsqlConnection(_connectionString);

    protected BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("ConnectionStrings")["DbConnectionString"]!;
    }
}