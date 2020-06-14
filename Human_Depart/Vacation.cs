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
    public partial class Vacation : Form
    {
        public static MySqlConnection con1 = new MySqlConnection(@"server=127.0.0.1;user id=root;persistsecurityinfo=True;database=human_depart;allowzerodatetime=True");
        public Vacation()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }
        public int numberorderv;
        private void LoadDataIntoDataGridView()
        {
            MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());
            con.Open();

            MySqlCommand cmd;

            cmd = con.CreateCommand();
            cmd.CommandText = "Select * from відпустка;";
            MySqlDataReader sd = cmd.ExecuteReader();

            DataTable dataT = new DataTable();

            dataT.Load(sd);

            WorkerDataGrid.DataSource = dataT;



        }
        public void cc()
        {
            Numworker.Items.Clear();
            con1.Open();
            MySqlCommand cm = con1.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "select * from працівник";
            cm.ExecuteNonQuery();
            DataTable dataTT = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            da.Fill(dataTT);
            foreach (DataRow dr in dataTT.Rows)
            {
                Numworker.Items.Add(dr["Номер_працівника"].ToString());
            }

            con1.Close();
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                con.Open();

                MySqlCommand cmd;

                cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO відпустка(Дата_початку,Дата_кінця,Причина,Номер_працівника,ПІБ_працівника) VALUES(@dateb,@dateend, @cause,@idw,@PIBW)";
                cmd.Parameters.AddWithValue("@dateb", DateTime.TryParse(datebtxt.Text, out var birthday) ? birthday : DateTime.Parse("1980/01/01"));
                cmd.Parameters.AddWithValue("@dateend", DateTime.TryParse(dateendtxt.Text, out var birthdayy) ? birthdayy : DateTime.Parse("1980/01/01"));
                cmd.Parameters.AddWithValue("@cause",cause.Text );
                cmd.Parameters.AddWithValue("@idw", Numworker.Text);
                 cmd.Parameters.AddWithValue("@PIBW", Pibtxt.Text);
              
                cmd.ExecuteNonQuery();

                con.Close();
             

                MessageBox.Show("Данні додані до бази", "Успішно");

            }
        }
        private bool IsValid()
        {

            CultureInfo ci = new CultureInfo("en-IE");
            if (cause.Text.Trim() == string.Empty)
            {
                MessageBox.Show("ПІБ потрібно заповнити", "Помилка поля");
                return false;
            }
            else if (!DateTime.TryParseExact(datebtxt.Text, "yyyy,MM,dd", ci, DateTimeStyles.None, out var rs))
            {

                MessageBox.Show(" вводи дату", "Помилка поля");
                return false;
            }
            else if (!DateTime.TryParseExact(dateendtxt.Text, "yyyy,MM,dd", ci, DateTimeStyles.None, out var rp))
            {

                MessageBox.Show(" вводи дату", "Помилка поля");
                return false;
            }
            
            return true;
        }

        private void Updated_Click(object sender, EventArgs e)
        {

            if (numberorderv != 0)
            {
                MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                con.Open();

                MySqlCommand cmd;

                cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE відпустка set Дата_початку= @dateb,Дата_кінця=@dateend,Причина=@cause,Номер_працівника=@idw,ПІБ_працівника=@PIBW WHERE Номер_наказу_про_відпустку =@id";
                cmd.Parameters.AddWithValue("@dateb", DateTime.TryParse(datebtxt.Text, out var birthday) ? birthday : DateTime.Parse("1980/01/01"));
                cmd.Parameters.AddWithValue("@dateend", DateTime.TryParse(dateendtxt.Text, out var birthdayy) ? birthdayy : DateTime.Parse("1980/01/01"));
                cmd.Parameters.AddWithValue("@cause", cause.Text);
                cmd.Parameters.AddWithValue("@idw", Numworker.Text);
                cmd.Parameters.AddWithValue("@PIBW", Pibtxt.Text);
                cmd.Parameters.AddWithValue("@id", numberorderv);

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

            if (numberorderv != 0)
            {
                if (numberorderv != 0)
                {
                    MySqlConnection con = new MySqlConnection(Adddata.ConnectionString());

                    con.Open();

                    MySqlCommand cmd;

                    cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM відпустка WHERE Номер_наказу_про_відпустку=@id";
                    cmd.Parameters.AddWithValue("@id", numberorderv);

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

            numberorderv = 0;
            Pibtxt.Clear();
            datebtxt.Refresh();
            dateendtxt.Clear();
            cause.Clear();
            Numworker.Refresh();
            Pibtxt.Clear();

        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                DGVPrinter print = new DGVPrinter();
                print.Title = "Звіт про відпустку";
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

                if (CAUSER.Checked)
                {
                    cmd.CommandText = "Select * FROM відпустка WHERE Причина =@cause";
                    cmd.Parameters.AddWithValue("@cause", SearchW.Text);
                }
                else if (PIBR.Checked)
                {
                    cmd.CommandText = "Select * from відпустка where ПІБ_працівника =@pib";
                    cmd.Parameters.AddWithValue("@pib", SearchW.Text);
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
            numberorderv = Convert.ToInt32(WorkerDataGrid.SelectedRows[0].Cells[0].Value);
            datebtxt.Text = WorkerDataGrid.SelectedRows[0].Cells[1].Value.ToString();
            dateendtxt.Text = WorkerDataGrid.SelectedRows[0].Cells[2].Value.ToString();
            cause.Text = WorkerDataGrid.SelectedRows[0].Cells[3].Value.ToString();
            Numworker.Text = WorkerDataGrid.SelectedRows[0].Cells[4].Value.ToString();
            Pibtxt.Text = WorkerDataGrid.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void Numworker_SelectedIndexChanged(object sender, EventArgs e)
        {
            con1.Open();
            MySqlCommand cm = con1.CreateCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "select * from працівник where Номер_працівника = '" + Numworker.SelectedItem.ToString() + "'";
            cm.ExecuteNonQuery();
            DataTable dataTT = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cm);
            da.Fill(dataTT);
            foreach (DataRow dr in dataTT.Rows)
            {
                Pibtxt.Text = dr["ПІБ_працівника"].ToString();
               

            }

            con1.Close();
        }

        private void Vacation_Load(object sender, EventArgs e)
        {
            cc();
        }
    }
}
