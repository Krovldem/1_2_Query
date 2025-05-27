using System;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class StaticFormExample : Form
    {
        public StaticFormExample()
        {
            InitializeComponent();

            btnShow.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string connString = ("Host=localhost;" +
                "Port=5432;" +
                "Database=stud_session;" +
                "User Id= yourID;" +
                "Password= yourPasswoord ;");

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
                        TextBoxResult.Text = $"Обновлено записей: {rowsAffected}";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Можно оставить пустым, если не требуется логика при вводе текста
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
