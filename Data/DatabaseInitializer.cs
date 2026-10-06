using Microsoft.Data.Sqlite;

namespace Leanny_P1_P4.Data;

public class DatabaseInitializer
{
    private readonly Database _database;

    public DatabaseInitializer(Database database)
    {
        _database = database;
    }

    public async Task InitializeAsync()
    {
        using var connection = _database.CreateConnection();

        await connection.OpenAsync();

        var command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Autores
            (
                IdAutor INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombres TEXT NOT NULL,
                Nacionalidad TEXT NOT NULL,
                FechaNacimiento TEXT NOT NULL,
                Sueldo REAL NOT NULL
            );
            """;

        await command.ExecuteNonQueryAsync();
    }
}