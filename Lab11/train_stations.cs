using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Lab11
{
    public partial class train_stations : Form
    {
        private OleDbConnection connection;

        public train_stations()
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
                string query = "INSERT INTO Вокзали (Назва, Адреса) VALUES (@Назва, @Адреса)";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@Назва", textBox1.Text);
                command.Parameters.AddWithValue("@Адреса", textBox2.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Інформація про вокзал успішно додана до бази даних.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка додавання даних: " + ex.Message);
            }
            finally
            {
                connection.Close();
                this.Close();
            }
        }
    }
}