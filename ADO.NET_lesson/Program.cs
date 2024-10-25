using MySql.Data.MySqlClient;
using System.Reflection;

public class Program
{
    public static void Main()
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";

        // Использование блока using для автоматического закрытия соединения
        using (var connection = new MySqlConnection(connectionString))
        {
            // Открытие соединения
            connection.Open();
            Console.WriteLine("Подключение открыто");

            // Вывод информации о подключении
            PrintProperties(connection);
        }
        Console.WriteLine("Подключение закрыто");
    }

    // Метод для вывода всех свойств объекта
    private static void PrintProperties(object obj)
    {
        // Получение типа объекта
        Type type = obj.GetType();
        // Получение всех свойств объекта
        PropertyInfo[] properties = type.GetProperties();

        // Перебор всех свойств
        foreach (var property in properties)
        {
            // Получение значения свойства
            object value = property.GetValue(obj, null);
            // Вывод имени и значения свойства
            Console.WriteLine($"{property.Name}: {value}");
        }
    }
}