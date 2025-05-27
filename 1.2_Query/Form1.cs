using System;
using System.Windows.Forms;
using Npgsql;

namespace SampleCommandStaticPG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Статический запрос PostgreSQL";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connStr = "Host=localhost;Port=5432;Username=postgres;Password=your_password;Database=Сессия";
            using (var conn = new NpgsqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE student
                        SET scholarship = scholarship * 1.1
                        WHERE birthday > '2005-01-01'";

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        textBoxResult.Text = $"Обновлено записей: {rowsAffected}";
                    else
                        textBoxResult.Text = "Изменений нет.";
                }
                catch (Exception ex)
                {
                    textBoxResult.Text = $"Ошибка: {ex.Message}";
                }
            }
        }
    }
}
