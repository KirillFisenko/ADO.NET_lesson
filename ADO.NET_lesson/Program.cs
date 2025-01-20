using MySql.Data.MySqlClient;

public class Program
{
    public static void Main()
    {
        var connectionString = "Server=localhost;Database=test;Uid=root;Pwd=m48kHz16bit%";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var showIndexQuery = "SHOW INDEX FROM users;";
        using var command = new MySqlCommand(showIndexQuery, connection);
        using MySqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("+----------------+------------------+----------------+");
        Console.WriteLine("|     Table      |    Key_name      |  Column_name   |");
        Console.WriteLine("+----------------+------------------+----------------+");

        while (reader.Read())
        {
            var table = reader["Table"].ToString().PadRight(14);
            var keyName = reader["Key_name"].ToString().PadRight(16);
            var columnName = reader["Column_name"].ToString().PadRight(14);

            Console.WriteLine($"| {table} | {keyName} | {columnName} |");
        }
        Console.WriteLine("+----------------+------------------+----------------+");
    }
}
