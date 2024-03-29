using Hotelio.Modules.Availability.Application.ReadModel;
using Microsoft.Data.SqlClient;

namespace Hotelio.Modules.Availability.Infrastructure.ReadModel;

internal class SqlServerResourceStorage: IResourceStorage
{
    private readonly string _connectionString;

    public SqlServerResourceStorage(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Resource?> FindFirstAvailableInDatesAsync(string group, int type, DateTime startDate, DateTime endDate)
    {
        Resource? resource = null;

       await using (var connection = new SqlConnection(_connectionString))
        {
            string query = @"
                            SELECT TOP 1 r.*
                            FROM availability.Resources r
                            WHERE r.GroupId = @GroupId AND r.Type = @Type AND IsActive = @Active
                            AND NOT EXISTS (
                                SELECT 1
                                FROM availability.Book b
                                WHERE b.ResourceId = r.Id
                                AND (
                                    (b.StartDate < @EndDate AND b.EndDate > @StartDate)
                                    OR (@StartDate < b.EndDate AND @EndDate > b.StartDate)
                                )
                            )
                            ORDER BY r.Id;
                        ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@GroupId", group);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate", endDate);
            command.Parameters.AddWithValue("@Active", true);

            connection.Open();
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.Read())
            {
                var id = (Guid)reader["Id"];
                resource = new Resource(
                    id.ToString(), 
                    (string)reader["GroupId"], 
                    (int)reader["Type"],
                    (bool)reader["IsActive"]);
            }

            reader.Close();
        }

        return resource;
    }
}