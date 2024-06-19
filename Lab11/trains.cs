using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Lab11
{
    public partial class trains : Form
    {
        private OleDbConnection connection;

        public trains()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=|DataDirectory|\Database1.accdb;";
            connection = new OleDbConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Потяги (Номер_потягу, Маршрут, Графік_руху) VALUES (@НомерПотягу, @Маршрут, @ГрафікРуху)";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@Номер_потягу", textBox1.Text);
                command.Parameters.AddWithValue("@Маршрут", textBox2.Text);
                command.Parameters.AddWithValue("@Графік_руху", textBox3.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Інформація про потяг успішно додана до бази даних.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка додавання інформації про потяг: " + ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close();
            }
        }
    }
}