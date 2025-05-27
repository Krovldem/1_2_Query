ОРГАНИЗАЦИЯ ЗАПРОСОВ К БАЗАМ ДАННЫХ
===================================

**Организации запросов к базе данных PostgreSQL из клиентского
приложения** на C\# с использованием библиотеки `Npgsql` с учетом всех
особенностей PostgreSQL и классов .NET:

------------------------------------------------------------------------

Организация запросов к БД PostgreSQL (на примере базы данных **Сессия**)
------------------------------------------------------------------------

При работе с PostgreSQL из клиентских приложений (например, написанных
на C\#) используется библиотека **Npgsql**, которая предоставляет
аналоги классов из пространства имен `System.Data.SqlClient` для MS SQL
Server. Основной класс для отправки SQL-запросов — **`NpgsqlCommand`**.

------------------------------------------------------------------------

Виды SQL-запросов
-----------------

### 1. **Статические запросы**

Текст запроса формируются полностью на этапе разработки.

**Пример:**

``` csharp
var cmd = new NpgsqlCommand("SELECT * FROM student", conn);
```

### 2. **Параметрические запросы**

Текст запроса формируется на этапе разработки приложения и содержит
несколько параметров, во время выполнения приложения можно задавать
значения параметров;

**Пример:**

``` csharp
var cmd = new NpgsqlCommand("SELECT * FROM student WHERE code = @code", conn);
cmd.Parameters.AddWithValue("code", 1);
```

### 3. **Динамические запросы**

Полный текст запроса формируется во время выполнения программы.

**Пример:**
`csharp   string tableName = "student";   var cmd = new NpgsqlCommand($"SELECT * FROM {tableName}", conn);`

------------------------------------------------------------------------

Класс `NpgsqlCommand`
---------------------

### Основные свойства:

-   **`Connection`** — объект подключения (`NpgsqlConnection`), через
    который будет выполняться запрос.
-   **`CommandText`** — текст SQL-запроса.
-   **`CommandType`** — тип команды (по умолчанию `CommandType.Text`):

    -   `Text` — обычный SQL-запрос.
    -   `StoredProcedure` — вызов хранимой процедуры.

### Создание объекта:

1.  Через конструктор:

    ``` csharp
    var cmd = new NpgsqlCommand("SELECT * FROM student", conn);
    ```

2.  Через метод подключения:

    ``` csharp
    var cmd = conn.CreateCommand();
    cmd.CommandText = "SELECT * FROM student";
    ```

------------------------------------------------------------------------

Методы выполнения запросов NpgsqlCommand
----------------------------------------

### 1. **ExecuteNonQuery()**

-   Используется для команд `INSERT`, `UPDATE`, `DELETE`, `CREATE`,
    `ALTER`, `DROP`.
-   Возвращает:

    -   количество затронутых строк (`int`);
    -   `-1`, если команда не возвращает информацию (например,
        `CREATE`).

``` csharp
var cmd = new NpgsqlCommand("INSERT INTO student (code, firstname, lastname) VALUES (20, 'Иван', 'Иванов')", conn);
int affectedRows = cmd.ExecuteNonQuery();
```

------------------------------------------------------------------------

### 2. **ExecuteScalar()**

-   Возвращает **первое поле первой строки** результата.
-   Применяется при агрегатных функциях (`COUNT`, `MAX`, `MIN`, `AVG`, и
    др.)

``` csharp
var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM student", conn);
int studentCount = (int)cmd.ExecuteScalar();
```

------------------------------------------------------------------------

### 3. **ExecuteReader()**

-   Возвращает объект **`NpgsqlDataReader`**, предназначенный для
    **построчного чтения данных** (только на чтение, прямой поток).
-   Используется с `SELECT`.

``` csharp
var cmd = new NpgsqlCommand("SELECT firstname, lastname FROM student", conn);
using (var reader = cmd.ExecuteReader())
{
    while (reader.Read())
    {
        string first = reader.GetString(0);  // или reader["firstname"].ToString()
        string last = reader["lastname"].ToString();
    }
}
```

#### Особенности `NpgsqlDataReader`:

-   Метод **`Read()`** продвигает курсор на одну строку вниз.
-   Начальное положение — перед первой строкой.
-   Доступ к полям — по индексу или имени.
-   После окончания чтения необходимо закрыть ридер: `reader.Close()`.

------------------------------------------------------------------------

Работа с параметрами (пример параметрического запроса):
-------------------------------------------------------

``` csharp
var cmd = new NpgsqlCommand("SELECT * FROM student WHERE code = @code", conn);
cmd.Parameters.AddWithValue("code", 1);
using (var reader = cmd.ExecuteReader())
{
    if (reader.Read())
    {
        Console.WriteLine(reader["firstname"]);
    }
}
```

------------------------------------------------------------------------

Создание статического запроса (PostgreSQL)
------------------------------------------

Рассмотрим пример создания статического запроса к базе данных
PostgreSQL. Пусть требуется увеличить размер стипендии студентов,
родившихся после 31 декабря 2005 года, на 10%.

Для этого создадим приложение, выполнив следующие шаги:

1.  Создайте новый проект Windows Forms с именем
    **SampleCommandStaticPostgres**.

2.  Измените заголовок формы, задав свойству `Text` значение
    **"Статический запрос (PostgreSQL)"**.

3.  Перетащите на форму с вкладки **Data** панели инструментов:

    -   кнопку с именем `btnShow`, установив свойство `Text` =
        **"Выполнить"**;
    -   текстовое поле с именем `TextBoxResult`, в которое будет
        выводиться результат выполнения запроса.

4.  Установите библиотеку `Npgsql` (например, через NuGet), и добавьте
    соответствующее пространство имён в начале файла:

    ``` csharp
    using Npgsql;
    ```

5.  Добавьте обработчик события нажатия на кнопку:

    ``` csharp
    string connString = ("Host=localhost;" +
        "Port=5432;" +
        "Database=stud_session_local;" +
        "User Id=yourID;" +
        "Password=yourPassword;");

    using (var conn = new NpgsqlConnection(connString))
    {
        try
        {
            conn.Open();

            string sql = @"
                UPDATE student
                SET scholarship = scholarship * 1.10
                WHERE birthday > '2005-12-31';
            ";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                    TextBoxResult.Text = "Изменено строк: " + rowsAffected.ToString();
                else
                    TextBoxResult.Text = "Нет изменений";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
        }
    }
    ```

6.  Запустите приложение.

После нажатия на кнопку в окне приложения отобразится результат –
текстовое сообщение с количеством изменённых записей или уведомление об
отсутствии изменений (см. рисунок 1).

![Описание изображения](pic1.png)

**Рисунок 1 – Приложение, использующее статический запрос в PostgreSQL**

------------------------------------------------------------------------

Если хочешь, могу помочь встроить этот блок прямо в твой текст.

В данном примере создаётся объект `cmd` класса `NpgsqlCommand`, с
помощью которого выполняется SQL-запрос на обновление методом
`ExecuteNonQuery()`. Результат выполнения (число затронутых строк)
сохраняется в переменной `rowsAffected`. В зависимости от результата в
текстовое поле выводится соответствующее сообщение.

Отметим, что текст запроса формируется заранее и не изменяется при
выполнении программы, поэтому он считается **статическим запросом**. Вот
теоретическое описание кода на C\# с использованием PostgreSQL и метода
`ExecuteReader()`, оформленное аналогично примеру с Microsoft SQL
Server:

------------------------------------------------------------------------

### Пример использования метода `ExecuteReader()` для выполнения запроса к базе данных PostgreSQL

Рассмотрим еще один пример создания статического запроса,
демонстрирующий использование метода `ExecuteReader()` **Npgsql**.

Пусть требуется получить список студентов, родившихся **после
31.12.2005**, из таблицы `student`. Для этого создадим приложение,
выполнив следующие действия:

1.  Создайте новый проект Windows Forms с именем, например,
    `ExecuteScalarFunc`.
2.  Измените свойство формы `Text`, например, на "Список студентов".
3.  Разместите на форме элементы управления:

    -   **Button** с именем `button1`, свойство `Text` = "Показать
        студентов";
    -   **TextBox** с именем `TextBoxResult`, свойство `Multiline` =
        `true`, для вывода результатов.

4.  Добавьте пространство имён:

    ``` csharp
    using Npgsql;
    ```

5.  Добавьте обработчик события `Click` для кнопки:

``` csharp
private void button1_Click(object sender, EventArgs e)
{
    string connectionString =
        "Host=localhost;" +
        "Port=5432;" +
        "Database=stud_session_local;" +
        "User Id=yourID;" +
        "Password=yourPassword";

    string query = "SELECT firstname, lastname, surname, birthday FROM student WHERE birthday > '2005-12-31' ORDER BY birthday";

    try
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            using (var reader = cmd.ExecuteReader())
            {
                TextBoxResult.Clear();
                while (reader.Read())
                {
                    string firstname = reader["firstname"].ToString();
                    string lastname = reader["lastname"].ToString();
                    string surname = reader["surname"].ToString();
                    string birthday = Convert.ToDateTime(reader["birthday"]).ToString("yyyy-MM-dd");

                    TextBoxResult.AppendText($"{firstname} {surname} {lastname} - {birthday}{Environment.NewLine}");
                }

                if (TextBoxResult.Text == "")
                    TextBoxResult.Text = "Нет студентов, родившихся после 31.12.2005";
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Ошибка: " + ex.Message);
    }
}
```

1.  Запустите приложение. После нажатия на кнопку, в `TextBoxResult`
    будет выведен список студентов, рожденных позже указанной даты.

После нажатия на кнопку в окне приложения отобразится результат –
текстовое сообщение с списком всех людей, чья дата рождения соотвествует указанной дате (см. рисунок 2).

![Описание изображения](pic1.png)

**Рисунок 1 – Приложение, использующее статический запрос в PostgreSQL**

------------------------------------------------------------------------

### Объяснение

В данном примере создается объект `NpgsqlCommand`, с помощью которого
выполняется SQL-запрос методом `ExecuteReader()`. Этот метод возвращает
результирующий набор данных в объект `reader` типа `NpgsqlDataReader`.

Для последовательного извлечения строк из набора используется метод
`Read()`. Обращение к полям текущей строки производится по имени
столбца:

``` csharp
reader["firstname"].ToString();
```

Полученные значения форматируются и выводятся в `TextBoxResult` с
помощью метода `AppendText()`.

Если ни одна запись не соответствует условию, пользователю выводится
сообщение: **"Нет студентов, родившихся после 31.12.2005"**.

------------------------------------------------------------------------

Создание параметрического запроса на C\# с использованием Npgsql
================================================================

Параметрический запрос — это SQL-запрос, в котором часть условий
задаётся через параметры. Такие запросы позволяют безопасно и удобно
передавать значения во время выполнения программы, предотвращая

Приведем пример выполнения такого запроса в приложении, которое получает
данные студентов и их оценки по предметам, где название предмета
соответствует введённому пользователем параметру.

Шаги создания приложения:
-------------------------

1.  Создайте новый проект Windows Forms с именем `ParamForm`.
2.  На форме разместите следующие элементы:

    -   TextBox с именем `txtParam` для ввода части названия предмета;
    -   Button с именем `btnShow` и текстом "Показать" для запуска
        запроса;
    -   ListBox с именем `listBoxResult` для отображения результатов.

3.  Добавьте ссылку на пакет `Npgsql` для работы с PostgreSQL.
4.  В коде формы реализуйте обработчик кнопки `btnShow_Click`, в котором
    происходит подключение к базе, создание параметрического запроса и
    вывод данных в ListBox.

------------------------------------------------------------------------

### Пример кода обработчика кнопки

``` csharp
private void btnShow_Click(object sender, EventArgs e)
{
    string connectionString =
         "Host=localhost;" +
        "Port=5432;" +
        "Database=stud_session_local;" +
        "User Id=yourID;" +
        "Password=yourPassword";

    using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
    {
        NpgsqlCommand cmd = con.CreateCommand();
        cmd.CommandText = @"
             SELECT s.firstname || ' ' || s.surname || ' ' || s.lastname AS fio,
                    sub.full_name AS subject,
                    sr.mark AS grade
               FROM session_results sr
               JOIN student s ON sr.student = s.code
               JOIN subject sub ON sr.subject = sub.code
              WHERE sub.full_name ILIKE '%' || @subject || '%' AND sr.mark >= 3
              ORDER BY s.firstname, s.surname;
        ";

        cmd.Parameters.AddWithValue("@subject", txtParam.Text);

        try
        {
            con.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            listBoxResult.Items.Clear();
            listBoxResult.Items.Add("ФИО - Предмет - Оценка");

            while (reader.Read())
            {
                string fio = reader["fio"].ToString();
                string subject = reader["subject"].ToString();
                string grade = reader["grade"].ToString();

                listBoxResult.Items.Add($"{fio} - {subject} - {grade}");
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }
}
```

1.  Запустите приложение

------------------------------------------------------------------------

### Объяснение

-   В строке подключения указываются параметры для подключения к
    локальному серверу PostgreSQL.
-   В `CommandText` прописан SQL-запрос, где часть условия (`@subject`)
    — параметр, значение которого берётся из текстового поля `txtParam`.
-   Метод `AddWithValue` добавляет параметр с именем `@subject` и
    значением из формы.
-   Выполнение запроса происходит в блоке `try-catch` для обработки
    возможных ошибок.
-   Результаты выводятся в ListBox построчно.

После нажатия на кнопку в окне приложения отобразится результат — список
записей, соответствующих введённому параметру, или сообщение об
отсутствии совпадений (см. рисунок 1).

![Описание изображения](pic2.png)

**Рисунок 3 – Результат выполнения параметрического запроса**

Вот обновлённый вариант вашего описания с местом для вставки рисунка по аналогии с вашим примером:

---

## Создание динамического запроса для PostgreSQL

Иногда текст SQL-запроса невозможно заранее определить на этапе разработки приложения. В таких случаях запрос формируется динамически во время выполнения программы. Такие запросы называются **динамическими**.

Для демонстрации работы с динамическим запросом создадим простое приложение, которое позволит пользователю самому вводить любое условие фильтрации данных из таблицы `session_results`.

### Шаги создания приложения:

1. Создайте новый проект Windows Forms с именем **SampleDynamic**.

2. Настройте форму:

   * Измените свойство формы **Text** на `"Динамический запрос к Сессии"`.
   * Добавьте с панели инструментов следующие элементы:

     * **Button** — имя `btnShow`, текст `"Выполнить"`.
     * **TextBox** — имя `txtParam`.
     * **ListBox** — имя `listBox1`.

3. Добавьте пространство имен для работы с PostgreSQL:

   ```csharp
   using Npgsql;
   ```

4. Добавьте код обработчика кнопки `btnShow`:

   ```csharp
   private void btnShow_Click(object sender, EventArgs e)
   {
       listBox1.Items.Clear();

       string userCondition = txtParam.Text.Trim();

       if (string.IsNullOrWhiteSpace(userCondition))
       {
           MessageBox.Show("Введите условие для фильтрации.");
           return;
       }

       string sql = "SELECT student, subject, teacher, date_of_exam, mark FROM session_results WHERE " + userCondition;

       try
       {
           using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
           using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
           {
               con.Open();
               using (NpgsqlDataReader reader = cmd.ExecuteReader())
               {
                   listBox1.Items.Add("------ Выполненный запрос ------");
                   listBox1.Items.Add(sql);
                   listBox1.Items.Add("-------------------------------");

                   while (reader.Read())
                   {
                       string line = $"Student: {reader["student"]}, Subject: {reader["subject"]}, Teacher: {reader["teacher"]}, Date: {Convert.ToDateTime(reader["date_of_exam"]).ToShortDateString()}, Mark: {reader["mark"]}";
                       listBox1.Items.Add(line);
                   }
               }
           }
       }
       catch (Exception ex)
       {
           MessageBox.Show("Ошибка выполнения запроса:\n" + ex.Message);
       }
   }
   ```

5. Запустите приложение. Введите в поле ввода, например, условие:

   ```
   mark > 4 AND date_of_exam >= '2024-01-01'
   ```

   и нажмите кнопку **Выполнить**.

   После нажатия на кнопку в окне приложения отобразится результат — список записей, соответствующих введённому параметру, или сообщение об отсутствии совпадений (см. рисунок 4).

   ![Описание изображения](pic4.png)

   **Рисунок 4 – Результат выполнения динамического запроса**

   

