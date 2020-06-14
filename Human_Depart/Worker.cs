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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {

                worksheet = workbook.ActiveSheet;

                worksheet.Name = "Лист1";

                int cellrowindex = 1;
                int cellcolumnindex = 1;


                for (int i = 0; i <= WorkerDataGrid.Rows.Count; i++)
                {
                    for (int j = 0; j < WorkerDataGrid.Columns.Count; j++)
                    {

                        if (cellrowindex == 1)
                        {
                            worksheet.Cells[cellrowindex, cellcolumnindex] = WorkerDataGrid.Columns[j].HeaderText;

                        }
                        else
                        {
                            worksheet.Cells[cellrowindex, cellcolumnindex] = WorkerDataGrid.Rows[i - 1].Cells[j].Value.ToString();
                        }
                        cellcolumnindex++;
                    }
                    cellcolumnindex = 1;
                    cellrowindex++;
                }


                SaveFileDialog savedialog = new SaveFileDialog();

                savedialog.Filter = "excel files (*.xlsx)|*.xlsx|all files (*.*)|*.*";
                savedialog.FilterIndex = 2;
                savedialog.FileName = "report_1"; //put file name here

                if (savedialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)

                {
                    workbook.SaveAs(savedialog.FileName);
                    MessageBox.Show("export successful");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (SearchW.Text.Trim() != string.Empty)
            {
                MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                con.Open();

                MySqlCommand cmd;

                cmd = con.CreateCommand();

                if (PIBR.Checked)
                {
                    cmd.CommandText = "Select * FROM працівник WHERE ПІБ_працівника =@pib";
                    cmd.Parameters.AddWithValue("@pib", SearchW.Text);
                }
                else if (PosadaR.Checked)
                {
                    cmd.CommandText = "Select * from працівник where Посада =@Position";
                    cmd.Parameters.AddWithValue("@Position", SearchW.Text);
                }
                MySqlDataReader sd = cmd.ExecuteReader();
                DataTable dataT = new DataTable();

                dataT.Load(sd);

                if (dataT.Rows.Count > 0)
                {
                    WorkerDataGrid.DataSource = dataT;
                }
                else
                {
                    MessageBox.Show("Не було знайдено жодного запису", "");
                }

            }
        }

        private void WorkerDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            worker_id = Convert.ToInt32(WorkerDataGrid.SelectedRows[0].Cells[0].Value);
            Pibtxt.Text = WorkerDataGrid.SelectedRows[0].Cells[1].Value.ToString();
            gendercb.Text = WorkerDataGrid.SelectedRows[0].Cells[2].Value.ToString();
            birthdaytxt.Text = WorkerDataGrid.SelectedRows[0].Cells[3].Value.ToString();
            positiontxt.Text = WorkerDataGrid.SelectedRows[0].Cells[4].Value.ToString();
            salarytxt.Text = WorkerDataGrid.SelectedRows[0].Cells[5].Value.ToString();
            Addresstxt.Text = WorkerDataGrid.SelectedRows[0].Cells[6].Value.ToString();
            phonenumbertxt.Text = WorkerDataGrid.SelectedRows[0].Cells[7].Value.ToString();

        }
    }
}
