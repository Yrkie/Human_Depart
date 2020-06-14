using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;
using DGVPrinterHelper;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Human_Depart
{
    public partial class Director : Form
    {
        public Director()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }
        public int worker_i;
        private void LoadDataIntoDataGridView()
        {
            MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());
            con.Open();

            MySqlCommand cmd;

            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from директор;";
            MySqlDataReader sd = cmd.ExecuteReader();

            DataTable dataT = new DataTable();

            dataT.Load(sd);

            WorkerDataGrid.DataSource = dataT;



        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                con.Open();

                MySqlCommand cmd;

                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO директор(ПІБ,Стать,Дата_народження) VALUES(@PIB,@gender, @Birthday)";
                cmd.Parameters.AddWithValue("@PIB", Pibtxt.Text);
                cmd.Parameters.AddWithValue("@gender", gendercb.Text);
                cmd.Parameters.AddWithValue("@Birthday", DateTime.TryParse(birthdaytxt.Text, out var birthday) ? birthday : DateTime.Parse("1980/01/01"));
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Данні додані до бази", "Успішно");

            }
        }
        private bool IsValid()
        {

            CultureInfo ci = new CultureInfo("en-IE");
            if (Pibtxt.Text.Trim() == string.Empty)
            {
                MessageBox.Show("ПІБ потрібно заповнити", "Помилка поля");
                return false;
            }
            else if (!DateTime.TryParseExact(birthdaytxt.Text, "yyyy,MM,dd", ci, DateTimeStyles.None, out var rs))
            {

                MessageBox.Show(" вводи дату", "Помилка поля");
                return false;
            }
            return true;
        }

        private void Updated_Click(object sender, EventArgs e)
        {

        }
    }
}
