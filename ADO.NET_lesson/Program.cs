using MySql.Data.MySqlClient;
using System.Data;

public class Program
{
    public static void Main1()
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=";

        // Создание подключения с автоматическим закрытием соединения
        using var connection = new MySqlConnection(connectionString);

        // Открытие соединения
        connection.Open();

        // Составление SQL-выражения для чтения данных из таблицы
        string sqlQuery = "SELECT * FROM USERS;";

        // Создание объекта для инкапсуляции выполняемого SQL-выражения 
        using MySqlCommand command = new MySqlCommand(sqlQuery, connection);

        // Создание объекта, который используется для чтения данных
        using MySqlDataReader reader = command.ExecuteReader();

        // Чтение строк из набора данных
        while (reader.Read())
        {
            // Получение значений столбцов и их форматирование для ровного вывода
            var value_id = reader["id"].ToString().PadRight(10);
            var value_firstName = reader["first_name"].ToString().PadRight(15);
            var value_lastName = reader["last_name"].ToString().PadRight(15);
            var value_email = reader["email"].ToString().PadRight(26);
            var value_age = reader["age"].ToString().PadRight(8);
            var value_createdAt = reader["created_at"].ToString().PadRight(20);

            // Вывод значений столбцов
            Console.WriteLine($"{value_id} {value_firstName} {value_lastName} {value_email} {value_age} {value_createdAt}");
        }
    }

    public static void Main2()
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=m48kHz16bit%;";

        // Создание подключения с автоматическим закрытием соединения
        using var connection = new MySqlConnection(connectionString);

        // Составление SQL-выражения для чтения данных из таблицы
        string sqlQuery = "SELECT * FROM USERS;";

        // Создание SqlDataAdapter для заполнения DataSet
        var dataAdapter = new MySqlDataAdapter(sqlQuery, connection);

        // Создание объекта DataSet, который хранит данные в памяти в виде таблиц
        var dataSet = new DataSet();

        // Заполнение DataSet данными
        dataAdapter.Fill(dataSet);

        // Зададим отступ для колонок
        var indent = 14;

        // Перебор таблиц
        foreach (DataTable table in dataSet.Tables)
        {
            // Перебор заголовков столбцов
            foreach (DataColumn column in table.Columns)
            {
                Console.Write(column.ColumnName.PadRight(indent));
            }
            Console.WriteLine();

            // Перебор строк
            foreach (DataRow row in table.Rows)
            {
                // Вывод значений ячеек
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item.ToString().PadRight(indent));
                }
                Console.WriteLine();
            }
        }
    }
}