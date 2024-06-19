using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Lab11
{
    public partial class passengers : Form
    {
        private OleDbConnection connection;

        public passengers()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=|DataDirectory|\Database1.accdb;";
            connection = new OleDbConnection(connectionString);
        }
        

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Пасажири (Прізвище, Ім_я, Адреса, Телефон) VALUES (@Прізвище, @Ім_я, @Адреса, @Телефон)";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@Прізвище", textBox1.Text);
                command.Parameters.AddWithValue("@Ім_я", textBox2.Text);
                command.Parameters.AddWithValue("@Адреса", textBox3.Text);
                command.Parameters.AddWithValue("@Телефон", textBox4.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Інформація про пасажира успішно додана до бази даних.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка додавання інформації про пасажира: " + ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close();
            }
        }
    }
}