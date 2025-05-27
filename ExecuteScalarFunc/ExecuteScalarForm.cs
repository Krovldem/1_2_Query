using System;
using System.Windows.Forms;
using Npgsql; 

namespace ExecuteScalarFunc
{
    public partial class ExecuteScalarForm : Form
    {
        public ExecuteScalarForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString =
                "Host=localhost;" +
                "Port=5432;" +
                "Database=stud_session_local;" +
                "Username=postgres;" +
                "Password=1205;";

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


        private void TextBoxResult_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
