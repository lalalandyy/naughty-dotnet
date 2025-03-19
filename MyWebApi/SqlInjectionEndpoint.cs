using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

public static class SqlInjectionEndpoint
{
    public static Results<Ok<string>, NotFound> GetUser(string userName, string password)
    {
        using var connection = new SqlConnection(Secrets.ConnectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT Name FROM Users WHERE Name = " + userName + " AND Password = " + password;
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return TypedResults.Ok(reader.GetString(0));
        }

        return TypedResults.NotFound();
    }
}
