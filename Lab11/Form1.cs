using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Lab11
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection;

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=|DataDirectory|\Database1.accdb;";
            connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();

                // Очищення контролів DataGridView перед завантаженням нових даних
                tabControl1.TabPages.Clear();

                // Отримання списку таблиць, ігноруючи системні таблиці
                DataTable schemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow row in schemaTable.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    // Ігнорувати системні таблиці
                    if (tableName.StartsWith("MSys")) continue;

                    TabPage tabPage = new TabPage(tableName);
                    tabControl1.TabPages.Add(tabPage);

                    DataGridView dataGridView = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
                    tabPage.Controls.Add(dataGridView);

                    OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{tableName}]", connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка підключення до бази даних: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            train_stations trainStationsForm = new train_stations();
            trainStationsForm.FormClosed += (s, args) => { LoadData(); };
            trainStationsForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            passengers passengersForm = new passengers();
            passengersForm.FormClosed += (s, args) => { LoadData(); };
            passengersForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            trains trainsForm = new trains();
            trainsForm.FormClosed += (s, args) => { LoadData(); };
            trainsForm.Show();
        }

        
    }
}