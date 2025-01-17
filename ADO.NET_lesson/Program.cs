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
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=;";

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

    public static void Main()
    {
        // Строка подключения к базе данных MySQL
        string connectionString = "Server=localhost;Database=test;Uid=root;Pwd=m48kHz16bit%;";

        // Создание подключения с автоматическим закрытием соединения
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        // Создаем таблицу в базе данных, если она не существует
        string createTableQuery = @"CREATE TABLE IF NOT EXISTS users (
                                    id INT AUTO_INCREMENT PRIMARY KEY,
                                    name VARCHAR(100) NOT NULL,
                                    age INT NOT NULL);";

        using var createTableCommand = new MySqlCommand(createTableQuery, connection);
        createTableCommand.ExecuteNonQuery();

        // Создаем объект DataSet с именем "MyDataSet"
        var dataSet = new DataSet("MyDataSet");

        // Создаем таблицу c именем "users" в DataSet
        var usersTable = new DataTable("users");

        // Создаем поля таблицы users с различными параметрами
        var idColumn = new DataColumn("id", typeof(int))
        {
            AutoIncrement = true, // автоинкрементный
            AutoIncrementSeed = 1, // начальное значение
            AutoIncrementStep = 1, // приращении при добавлении новой строки
            AllowDBNull = false, // не может принимать null
            Unique = true // столбец будет иметь уникальное значение
        };
        usersTable.Columns.Add(idColumn);

        var nameColumn = new DataColumn("name", typeof(string))
        {
            AllowDBNull = false, // не может принимать null
            MaxLength = 100 // ограничение длинны
        };
        usersTable.Columns.Add(nameColumn);

        var ageColumn = new DataColumn("age", typeof(int))
        {
            AllowDBNull = false, // не может принимать null
            DefaultValue = 0 // значение по умолчанию
        };
        usersTable.Columns.Add(ageColumn);

        // Определяем первичный ключ таблицы
        usersTable.PrimaryKey = [usersTable.Columns["id"]];

        // Добавляем данные в таблицу
        usersTable.Rows.Add(null, "Кирилл", 35);
        usersTable.Rows.Add(null, "Иосиф", 25);
        usersTable.Rows.Add(null, "Павел", 27);

        // Добавляем таблицу в DataSet
        dataSet.Tables.Add(usersTable);

        // Создаем MySqlDataAdapter для выполнения операций с базой данных
        var dataAdapter = new MySqlDataAdapter("SELECT * FROM users", connection);

        // Используем MySqlCommandBuilder для автоматической генерации команд
        var commandBuilder = new MySqlCommandBuilder(dataAdapter);

        // Обновляем базу данных данными из DataSet
        dataAdapter.Update(dataSet, "users");

        // Обновляем данные в таблице
        usersTable.Rows[0]["name"] = "Иван";
        usersTable.Rows[0]["age"] = 36;

        // Удаляем строку из таблицы
        usersTable.Rows[1].Delete();

        // Обновляем базу данных данными из DataSet
        dataAdapter.Update(dataSet, "users");
    }
}