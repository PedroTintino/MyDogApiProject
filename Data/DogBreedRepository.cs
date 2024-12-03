using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MyDogApiProject.Data;

public class DogBreedRepository : IDogBreedRepository
{
    private readonly string _connectionString;

    public DogBreedRepository(IConfiguration connectionString) 
    {
        _connectionString = connectionString["PostgresConnectionString"];
    }

    public async Task<List<string>> GetAllBreedsAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        const string query = "SELECT Name FROM DogBreeds;";
        return (await connection.QueryAsync<string>(query)).ToList();
    }

    public async Task<bool> BreedExistsAsync(string breedName)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        const string query = "SELECT COUNT(1) FROM DogBreeds WHERE Name = @Name;";
        return await connection.ExecuteScalarAsync<bool>(query, new { Name = breedName });
    }


    //equivalente a void
    public async Task AddBreedAsync(string breedName)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        const string query = "INSERT INTO DogBreeds (Name) VALUES (@Name);";
        await connection.ExecuteAsync(query, new { Name = breedName });
    }
}
