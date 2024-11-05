using MySql.Data.MySqlClient;
using System.Reflection;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введите имя:");
        var firstName = Console.ReadLine();

        Console.WriteLine("Введите фамилию:");
        var lastName = Console.ReadLine();

        Console.WriteLine("Введите email:");
        var email = Console.ReadLine();

        Console.WriteLine("Введите возраст:");
        var age = int.Parse(Console.ReadLine());

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
            using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
            {
                // Выполнение команды на создание таблицы
                var execute = command.ExecuteNonQuery();

                Console.WriteLine($"Создана таблица users. Количество измененных записей: {execute}");

                // Добавляем параметры запроса
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@age", age);

                // Переопределяем SQL-выражение, вставляем данные в таблицу
                command.CommandText = $@"INSERT INTO users (first_name, last_name, email, age) VALUES
                                (@firstName, @lastName, @email, @age);
                               ";

                // Выполнение команды на вставку данных
                execute = command.ExecuteNonQuery();

                Console.WriteLine($"Количество вставленных записей в таблицу users: {execute}");
            };
        }
    }

    //public static void Main()
    //{
    //    // Строка подключения к базе данных MySQL
    //    string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";

    //    // Создание подключения с автоматическим закрытием соединения
    //    using var connection = new MySqlConnection(connectionString);

    //    // Открытие соединения
    //    connection.Open();

    //    // Составление SQL-выражения для чтения данных из таблицы
    //    string sqlQuery = "SELECT * FROM USERS;";

    //    // Создание объекта для инкапсуляции выполняемого SQL-выражения 
    //    using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

    //    // Создание объекта, который используется для чтения данных
    //    using MySqlDataReader reader = command.ExecuteReader();

    //    // Проверка, содержит ли набор данных строки
    //    if (reader.HasRows)
    //    {
    //        // Получение имен столбцов и их форматирование для ровного вывода
    //        var columnName_id = "id".PadRight(10);
    //        var columnName_firstName = "имя".PadRight(15);
    //        var columnName_lastName = "фамилия".PadRight(15);
    //        var columnName_email = "email".PadRight(26);
    //        var columnName_age = "возраст".PadRight(8);
    //        var columnName_createdAt = "дата регистрации".PadRight(20);

    //        // Вывод заголовков столбцов
    //        Console.WriteLine($"{columnName_id} {columnName_firstName} {columnName_lastName} {columnName_email} {columnName_age} {columnName_createdAt}");

    //        // Чтение строк из набора данных
    //        while (reader.Read())
    //        {
    //            // Получение значений столбцов и их форматирование для ровного вывода
    //            var value_id = reader["id"].ToString().PadRight(10);
    //            var value_firstName = reader["first_name"].ToString().PadRight(15);
    //            var value_lastName = reader["last_name"].ToString().PadRight(15);
    //            var value_email = reader["email"].ToString().PadRight(26);
    //            var value_age = reader["age"].ToString().PadRight(8);
    //            var value_createdAt = reader["created_at"].ToString().PadRight(20);

    //            // Вывод значений столбцов
    //            Console.WriteLine($"{value_id} {value_firstName} {value_lastName} {value_email} {value_age} {value_createdAt}");
    //        }
    //    }
    //}

    /// Метод для вывода всех свойств объекта
    private static void PrintProperties(object obj)
    {
        // Получение типа объекта
        Type type = obj.GetType();

        // Получение всех свойств объекта
        PropertyInfo[] properties = type.GetProperties();

        // Перебор всех свойств
        foreach (var property in properties)
        {
            object value;

            // Проверка, является ли свойство индексатором
            if (property.GetIndexParameters().Length == 0)
            {
                // Если свойство не является индексатором, получаем его значение
                value = property.GetValue(obj, null);
            }
            else
            {
                // Если свойство является индексатором, выводим сообщение "Indexed property"
                value = "Indexed property";
            }

            // Вывод имени свойства и его значения
            Console.WriteLine($"{property.Name}: {value}");
        }
    }
}