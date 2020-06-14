using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Human_Depart
{
   
    public partial class LoginForm : Form
    {
        Mainform mainform = new Mainform();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Click(object sender, EventArgs e)
        {

            String User = Log.Text;
            String Pass = Password.Text;


            Database db = new Database();


            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `log` WHERE `Login`= @uL AND `Password`= @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = User;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = Pass;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                mainform.Show();
            }
        else
                MessageBox.Show("Неправильні дані");

            if (table.Rows.Count > 0)
                this.Hide();
        }

        private void Log_Click(object sender, EventArgs e)
        {
            Log.Clear();
        }

        private void Password_Click(object sender, EventArgs e)
        {
            Password.Clear();
        }
    }
}
