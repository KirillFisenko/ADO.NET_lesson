using MySql.Data.MySqlClient;

public class Program
{
    public static void Main()
    {
        var connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        using var command = new MySqlCommand();
        command.Connection = connection;

        // Нахождение количества пользователей старше 25 лет
        command.CommandText = "SELECT COUNT(id) FROM USERS WHERE age > 25;"; ;
        object usersOlder30 = command.ExecuteScalar();
        Console.WriteLine(usersOlder30);

        // Вывод полного имени самого старшего пользователя
        command.CommandText = "SELECT CONCAT(first_name, ' ', last_name) FROM USERS  WHERE age = (SELECT MAX(age) FROM USERS);"; ;
        object oldestUser = command.ExecuteScalar();
        Console.WriteLine(oldestUser);
    }
}