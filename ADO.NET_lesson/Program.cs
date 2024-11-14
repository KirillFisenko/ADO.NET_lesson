using MySql.Data.MySqlClient;

public class Program
{
    //public static void Main()
    //{
    //    var connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";
    //    using var connection = new MySqlConnection(connectionString);
    //    connection.Open();
    //    using var command = new MySqlCommand();
    //    command.Connection = connection;

    //    // Нахождение количества пользователей старше 25 лет
    //    command.CommandText = "SELECT COUNT(id) FROM USERS WHERE age > 25;"; ;
    //    object usersOlder30 = command.ExecuteScalar();
    //    Console.WriteLine(usersOlder30);

    //    // Вывод полного имени самого старшего пользователя
    //    command.CommandText = "SELECT CONCAT(first_name, ' ', last_name) FROM USERS  WHERE age = (SELECT MAX(age) FROM USERS);"; ;
    //    object oldestUser = command.ExecuteScalar();
    //    Console.WriteLine(oldestUser);
    //}

    //public static void Main()
    //{
    //    var firstName = "Василий";
    //    var lastName = "Петров";
    //    var email = "email@mail.ru";
    //    var age = 31;
    //    var connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";
    //    using var connection = new MySqlConnection(connectionString);
    //    connection.Open();
    //    var sqlQuery = $@"INSERT INTO users (first_name, last_name, email, age) VALUES
    //                                         (@firstName, @lastName, @email, @age);";
    //    using var command = new MySqlCommand(sqlQuery, connection);
    //    command.Parameters.AddWithValue("@firstName", firstName);
    //    command.Parameters.AddWithValue("@lastName", lastName);
    //    command.Parameters.AddWithValue("@email", email);
    //    command.Parameters.AddWithValue("@age", age);
    //    var execute = command.ExecuteNonQuery();
    //}

    public static void Main()
    {
        var firstName = "Василий";
        var lastName = "Петров";
        var email = "1email@mail.com";
        var age = 30;
        var connectionString = "Server=localhost;Database=test;Uid=root;Pwd=m48kHz16bit%;";
        using var connection = new MySqlConnection(connectionString);
        connection.Open();
        var sqlQuery = $@"INSERT INTO users (first_name, last_name, email, age) VALUES
                                             (@firstName, @lastName, @email, @age);";
        using var command = new MySqlCommand(sqlQuery, connection);

        // Создание параметра для имени
        var firstNameParam = new MySqlParameter
        {
            // Установка имени параметра
            ParameterName = "@firstName",

            // Установка значения параметра
            Value = firstName,

            // Установка типа данных параметра
            MySqlDbType = MySqlDbType.VarChar,

            // Установка размера параметра (в данном случае длина строки)
            Size = 100,

            // Установка флага, указывающего, может ли параметр быть NULL
            IsNullable = false
        };

        var lastNameParam = new MySqlParameter("@lastName", lastName);
        var emailParam = new MySqlParameter("@email", email);
        var ageParam = new MySqlParameter("@age", age);

        // Добавляем параметры для запроса
        command.Parameters.Add(firstNameParam);
        command.Parameters.Add(lastNameParam);
        command.Parameters.Add(emailParam);
        command.Parameters.Add(ageParam);

        // Выполнение запроса
        var execute = command.ExecuteNonQuery();
    }
}