using MySql.Data.MySqlClient;

public class Program
{
    public static void Main()
    {
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=m48kHz16bit%;";

        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Подключение открыто");

            // Вывод информации о подключении
            Console.WriteLine(@$"Свойства подключения:
            Строка подключения: {connection.ConnectionString}
            База данных: {connection.Database}
            Сервер: {connection.DataSource}
            Версия сервера: {connection.ServerVersion}
            Состояние: {connection.State}");
        }
        Console.WriteLine("Подключение закрыто");
    }
}