using MySql.Data.MySqlClient;

public class Program
{
    public static void Main()
    {
        var firstName = "firstName";
        var lastName = "lastName";
        var email = "email";
        var age = 30;
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        // Начало транзакции
        MySqlTransaction transaction = connection.BeginTransaction();

        try
        {
            // Выполнение операций в транзакции
            string sqlQuery = @"CREATE TABLE IF NOT EXISTS users (
                              id INT AUTO_INCREMENT PRIMARY KEY,
                              first_name VARCHAR(50) NOT NULL,
                              last_name VARCHAR(50) NOT NULL,
                              email VARCHAR(100) UNIQUE NOT NULL,
                              age INT,
                              created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP);";

            using MySqlCommand command = new MySqlCommand(sqlQuery, connection, transaction);
            command.ExecuteNonQuery();

            command.CommandText = @"INSERT INTO users (first_name, last_name, email, age) VALUES
                                                      (@firstName, @lastName, @email, @age);";
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@age", age);
            command.ExecuteNonQuery();

            // Все изменения сохраняются в базе данных
            transaction.Commit();
            Console.WriteLine("Transaction committed successfully.");
        }
        catch (Exception ex)
        {
            // Все изменения откатываются
            transaction.Rollback();
            Console.WriteLine("Transaction failed: " + ex.Message);
        }
    }
}