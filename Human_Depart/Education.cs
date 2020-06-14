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
    public partial class Education : Form
    {
        public Education()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }
        public int diplomnumber;
        private void LoadDataIntoDataGridView()
        {
            MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());
            con.Open();

            MySqlCommand cmd;

            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from освіта;";
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
                cmd.CommandText = "INSERT INTO освіта(Рік_отримання,Спеціальність) VALUES(@year,@spec)";
                cmd.Parameters.AddWithValue("@year", YearE.Text);
                cmd.Parameters.AddWithValue("@spec", Spec.Text);
               
                
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Данні додані до бази", "Успішно");

            }
        }
        private bool IsValid()
        {

            CultureInfo ci = new CultureInfo("en-IE");
            if (YearE.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Рік отримання потрібно заповнити", "Помилка поля");
                return false;
            }
            if (Spec.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Спеціальність потрібно заповнити", "Помилка поля");
                return false;
            }
            return true;
        }

        private void Updated_Click(object sender, EventArgs e)
        {

            if (diplomnumber != 0)
            {
                MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                con.Open();

                MySqlCommand cmd;

                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE освіта set Рік_отримання=@year,Спеціальність=@spec Where Номер_диплома=@id";

                cmd.Parameters.AddWithValue("@year", YearE.Text);
                cmd.Parameters.AddWithValue("@spec", Spec.Text);
               
                
                cmd.Parameters.AddWithValue("@id", this.diplomnumber);



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

            if (diplomnumber != 0)
            {
                if (diplomnumber != 0)
                {
                    MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                    con.Open();

                    MySqlCommand cmd;

                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM освіта WHERE Номер_диплома=@id";
                    cmd.Parameters.AddWithValue("@id", diplomnumber);

                    cmd.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Інформація про освіту працівника успішно видалена", "Успішно");

                    LoadDataIntoDataGridView();
                }
                else
                {
                    MessageBox.Show("Виберіть поле освіти яку ви хочете видалити", "Виберіть працівника");
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

            diplomnumber = 0;
            YearE.Clear();
            Spec.Clear();
           

        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                DGVPrinter print = new DGVPrinter();
                print.Title = "Звіт про освіту";
                print.SubTitle = "Дата друку: " + DateTime.Now.ToShortDateString();
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

                if (Special.Checked)
                {
                    cmd.CommandText = "Select * FROM освіта WHERE Спеціальність=@spec";
                    cmd.Parameters.AddWithValue("@spec", SearchW.Text);
                }
              else if (yearend.Checked)
                {
                    cmd.CommandText = "Select * FROM освіта WHERE Рік_отримання=@year";
                    cmd.Parameters.AddWithValue("@year", SearchW.Text);
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
            diplomnumber = Convert.ToInt32(WorkerDataGrid.SelectedRows[0].Cells[0].Value);
            YearE.Text = WorkerDataGrid.SelectedRows[0].Cells[1].Value.ToString();
            Spec.Text = WorkerDataGrid.SelectedRows[0].Cells[2].Value.ToString();


        }
    }
}
