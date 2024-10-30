using MySql.Data.MySqlClient;
using System.Reflection;

public class Program
{
    public static void Main()
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";

        // Создание подключения с автоматическим закрытием соединения
        using (var connection = new MySqlConnection(connectionString))
        {
            // Открытие соединения
            connection.Open();

            // Составление SQL-выражения на создание таблицы
            string sqlQuery = @"CREATE TABLE IF NOT EXISTS users (
                                  id INT AUTO_INCREMENT PRIMARY KEY,
                                  first_name VARCHAR(50) NOT NULL,
                                  last_name VARCHAR(50) NOT NULL,
                                  email VARCHAR(100) UNIQUE NOT NULL,
                                  age INT,
                                  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                               );";

            // Создание объекта для инкапсуляции выполняемого SQL-выражения 
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            // Выполнение команды на создание таблицы
            var execute = command.ExecuteNonQuery();

            Console.WriteLine($"Создана таблица users. Количество измененных записей: {execute}");

            // Переопределяем SQL-выражение, вставляем данные в таблицу
            command.CommandText = @"INSERT INTO users (first_name, last_name, email, age) VALUES
                                    ('John', 'Doe', 'john.doe@example.com', 30),
                                    ('Jane', 'Smith', 'jane.smith@example.com', 25),
                                    ('Alice', 'Johnson', 'alice.johnson@example.com', 28),
                                    ('Bob', 'Brown', 'bob.brown@example.com', 35),
                                    ('Charlie', 'Davis', 'charlie.davis@example.com', 22);
                                   ";

            // Выполнение команды на вставку данных
            execute = command.ExecuteNonQuery();

            Console.WriteLine($"Количество вставленных записей в таблицу users: {execute}");
        }
    }

    // Метод для вывода всех свойств объекта
    private static void PrintProperties(object obj)
    {
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (var property in properties)
        {
            object value = property.GetValue(obj, null);
            Console.WriteLine($"{property.Name}: {value}");
        }
    }
}