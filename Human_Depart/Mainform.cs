using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Human_Depart
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }
        private void customizeDesign()
        {
            panelMediaSubMenu.Visible = false;

        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        private void hideSubMenu()
        {
            if (panelMediaSubMenu.Visible == true)
                panelMediaSubMenu.Visible = false;

        }
        private void BtnTable_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }
        private Form activeForm = null;
        public void openNewForm(Form mainform)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = mainform;
            mainform.TopLevel = false;
            mainform.FormBorderStyle = FormBorderStyle.None;
            mainform.Dock = DockStyle.Fill;
            Mainpanel.Controls.Add(mainform);
            Mainpanel.Tag = mainform;
            mainform.BringToFront();
            mainform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openNewForm(new Worker());
            hideSubMenu();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openNewForm(new Director());
            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openNewForm(new Education());
            hideSubMenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            openNewForm(new Vacation());
            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openNewForm(new Hospital());
            hideSubMenu();
        }
    }
}
