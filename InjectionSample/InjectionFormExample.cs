using System;
using System.Windows.Forms;
using Npgsql;

namespace SampleSQLInjectionPostgres
{
    public partial class InjectionFormExample : Form
    {
        private string connectionString =
                 "Host=localhost;" +
                "Port=5432;" +
                "Database=stud_session_local;" +
                "Username=postgres;" +
                "Password=1205;";

        public InjectionFormExample()
        {
            InitializeComponent();
        }

        private void buttonDangerous_Click(object sender, EventArgs e)
        {
            string query =
                "INSERT INTO session_results (student, subject, teacher, date_of_exam, mark) VALUES (" +
                TextStudent.Text + ", " +
                TextSubject.Text + ", " +
                TextTeacher.Text + ", '" +
                TextDateOfExam.Text + "', " +
                TextMark.Text + ")";

            using (var con = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись добавлена (опасно).");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void buttonSafe_Click(object sender, EventArgs e)
        {
            string query =
                "INSERT INTO session_results (student, subject, teacher, date_of_exam, mark) " +
                "VALUES (@student, @subject, @teacher, @date_of_exam, @mark)";

            using (var con = new NpgsqlConnection(connectionString))
            using (var cmd = new NpgsqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("student", int.Parse(TextStudent.Text));
                cmd.Parameters.AddWithValue("subject", int.Parse(TextSubject.Text));
                cmd.Parameters.AddWithValue("teacher", int.Parse(TextTeacher.Text));
                cmd.Parameters.AddWithValue("date_of_exam", DateTime.Parse(TextDateOfExam.Text));
                cmd.Parameters.AddWithValue("mark", int.Parse(TextMark.Text));

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Запись добавлена (безопасно).");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}
