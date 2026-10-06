using Dapper;
using Leanny_P1_P4.Data;
using Leanny_P1_P4.Models;

namespace Leanny_P1_P4.Services;

public class AutoresService
{
    private readonly Database _database;

    public AutoresService(Database database)
    {
        _database = database;
    }

    public async Task<int> SaveAsync(Autor autor)
    {
        using var connection = _database.CreateConnection();

        const string query = """
            INSERT INTO Autores
            (
                Nombres,
                Nacionalidad,
                FechaNacimiento,
                Sueldo
            )
            VALUES
            (
                @Nombres,
                @Nacionalidad,
                @FechaNacimiento,
                @Sueldo
            );

            SELECT last_insert_rowid();
            """;

        return await connection.ExecuteScalarAsync<int>(query, autor);
    }

    public async Task<IEnumerable<Autor>> GetListAsync()
    {
        using var connection = _database.CreateConnection();

        const string query = """
            SELECT
                IdAutor,
                Nombres,
                Nacionalidad,
                FechaNacimiento,
                Sueldo
            FROM Autores
            ORDER BY IdAutor ASC;
            """;

        return await connection.QueryAsync<Autor>(query);
    }

    public async Task<Autor?> GetByIdAsync(int id)
    {
        using var connection = _database.CreateConnection();

        const string query = """
            SELECT
                IdAutor,
                Nombres,
                Nacionalidad,
                FechaNacimiento,
                Sueldo
            FROM Autores
            WHERE IdAutor = @IdAutor;
            """;

        return await connection.QueryFirstOrDefaultAsync<Autor>(
            query,
            new { IdAutor = id });
    }

    public async Task<bool> UpdateAsync(int id, Autor autor)
    {
        using var connection = _database.CreateConnection();

        const string query = """
            UPDATE Autores
            SET
                Nombres = @Nombres,
                Nacionalidad = @Nacionalidad,
                FechaNacimiento = @FechaNacimiento,
                Sueldo = @Sueldo
            WHERE IdAutor = @IdAutor;
            """;

        var filasAfectadas = await connection.ExecuteAsync(
            query,
            new
            {
                IdAutor = id,
                autor.Nombres,
                autor.Nacionalidad,
                autor.FechaNacimiento,
                autor.Sueldo
            });

        return filasAfectadas > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _database.CreateConnection();

        const string query = """
            DELETE FROM Autores
            WHERE IdAutor = @IdAutor;
            """;

        var filasAfectadas = await connection.ExecuteAsync(
            query,
            new { IdAutor = id });

        return filasAfectadas > 0;
    }
}
