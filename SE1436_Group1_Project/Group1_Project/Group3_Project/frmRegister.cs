using Group1_Project.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }
        //register
        private void button1_Click(object sender, EventArgs e)
        {
            //check error and show message
            if (textBox1.Text.Equals("")) MessageBox.Show("Username cannot blank!");
            else if (textBox2.Text.Equals("")) MessageBox.Show("Password cannot blank!");
            else if(textBox3.Text.Equals("")) MessageBox.Show("Please confirm password!");
            else if (textBox2.Text != textBox3.Text)
            {
                label7.Visible = true;
                label7.ForeColor = Color.Red;
                label7.Text = "Password and Confirm Password doesn't match!";
            }
            else
            {
                //check account exitst or not
                if (DAO.checkAccountExist(textBox1.Text) == true)
                {
                    label7.Visible = true;
                    label7.ForeColor = Color.Red;
                    label7.Text = "Username already exist!Please choose another name!";
                }
                else
                {
                    label7.Visible = false;
                    DAO.insertAccount(textBox1.Text, textBox2.Text);
                    label7.Visible = true;
                    label7.ForeColor = Color.Green;
                    label7.Text = "Create account successfully";
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmLogin().ShowDialog();
        }
    }
}
