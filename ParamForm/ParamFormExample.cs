using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace ParamForm
{
    public partial class ParamFormExample : Form
    {
        public ParamFormExample()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
      
        }

        private void txtParam_TextChanged(object sender, EventArgs e)
        {
            // По желанию
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string connectionString =
                 "Host=localhost;" +
                "Port=5432;" +
                "Database=stud_session_local;" +
                "Username=postgres;" +
                "Password=1205;";

            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                NpgsqlCommand cmd = con.CreateCommand();
                cmd.CommandText = @"
                     SELECT s.firstname || ' ' || s.surname || ' ' || s.lastname AS fio, sub.full_name AS subject, sr.mark AS grade
                       FROM session_results sr
                        JOIN student s ON sr.student = s.code
                        JOIN subject sub ON sr.subject = sub.code
                           WHERE sub.full_name ILIKE '%' || @subject || '%' AND sr.mark >= 3
                           ORDER BY s.firstname, s.surname;
                ";

                cmd.Parameters.AddWithValue("@subject", "%" + txtParam.Text + "%");

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
    }
}
