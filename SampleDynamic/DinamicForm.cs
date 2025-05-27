using System;
using System.Windows.Forms;
using Npgsql;

namespace SampleDynamic
{
    public partial class ListOf : Form
    {
        private string connectionString =
             "Host=localhost;" +
                "Port=5432;" +
                "Database=stud_session_local;" +
                "Username=postgres;" +
                "Password=1205;";

        public ListOf()
        {
            InitializeComponent();
        }

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

        private void ListOf_Load(object sender, EventArgs e)
        {
            // Можно добавить инициализацию, если требуется
        }
    }
}
