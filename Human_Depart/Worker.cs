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
    public partial class Worker : Form
    {
        public Worker()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }
        public int worker_id;
        private void LoadDataIntoDataGridView()
        {
            MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());
            con.Open();

            MySqlCommand cmd;

            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from працівник;";
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
                cmd.CommandText = "INSERT INTO працівник(ПІБ_працівника,Стать,Дата_народження,Посада,Зарплатня,Адреса,Номер_телефону) VALUES(@PIB,@gender, @Birthday,@Position,@salary,@Address,@phonenumber)";
                cmd.Parameters.AddWithValue("@PIB", Pibtxt.Text);
                cmd.Parameters.AddWithValue("@gender", gendercb.Text);
                cmd.Parameters.AddWithValue("@Birthday", DateTime.TryParse(birthdaytxt.Text, out var birthday) ? birthday : DateTime.Parse("1980/01/01"));
                cmd.Parameters.AddWithValue("@Position", positiontxt.Text);
                cmd.Parameters.AddWithValue("@salary", salarytxt.Text);
                cmd.Parameters.AddWithValue("@Address", Addresstxt.Text);
                cmd.Parameters.AddWithValue("@phonenumber", phonenumbertxt.Text);
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

            if (worker_id != 0)
            {
                MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                con.Open();

                MySqlCommand cmd;

                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE працівник set ПІБ_працівника=@PIB,Стать=@gender,Дата_народження=@Birthday,Посада=@Position,Зарплатня=@Salary,Адреса=@Address,Номер_телефону=@phonenumber WHERE Номер_працівника =@id";

                cmd.Parameters.AddWithValue("@PIB", Pibtxt.Text);
                cmd.Parameters.AddWithValue("@gender", gendercb.Text);
                cmd.Parameters.AddWithValue("@Birthday", DateTime.TryParse(birthdaytxt.Text, out var birthday) ? birthday : DateTime.Parse("1980/01/01"));
                cmd.Parameters.AddWithValue("@Position", positiontxt.Text);
                cmd.Parameters.AddWithValue("@salary", salarytxt.Text);
                cmd.Parameters.AddWithValue("@Address", Addresstxt.Text);
                cmd.Parameters.AddWithValue("@phonenumber", phonenumbertxt.Text);
                cmd.Parameters.AddWithValue("@id", this.worker_id);



                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("База обновлена", "Успішно");

                LoadDataIntoDataGridView();
            }
            else
            {
                MessageBox.Show("Будь ласка, виділіть запис", "Помилка");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {

            if (worker_id != 0)
            {
                if (worker_id != 0)
                {
                    MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                    con.Open();

                    MySqlCommand cmd;

                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM працівник WHERE Номер_працівника=@id";
                    cmd.Parameters.AddWithValue("@id", worker_id);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Працівник успішно видален", "Успішно");

                    LoadDataIntoDataGridView();
                }
                else
                {
                    MessageBox.Show("Виберіть працівника якого ви хочете видалити", "Виберіть працівника");
                }
            }
        }

        private void Drop_Click(object sender, EventArgs e)
        {
            ResetFormData();
            LoadDataIntoDataGridView();
        }
        private void ResetFormData()
        {

            worker_id = 0;
            Pibtxt.Clear();
            birthdaytxt.Refresh();
            phonenumbertxt.Clear();
            Addresstxt.Clear();
            salarytxt.Clear();
            positiontxt.Clear();

        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                DGVPrinter print = new DGVPrinter();
                print.Title = "Звіт про працівників";
                print.SubTitle = "Print Date: " + DateTime.Now.ToShortDateString();
                print.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                print.PageNumbers = true;
                print.PageNumberInHeader = false;
                print.PorportionalColumns = true;
                print.HeaderCellAlignment = StringAlignment.Near;



                // print.PrintDataGridView(WorkerDataGrid);
                print.PrintPreviewDataGridView(WorkerDataGrid);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
