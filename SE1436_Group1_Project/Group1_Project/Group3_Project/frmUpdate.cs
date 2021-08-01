using Group1_Project.DAO;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmUpdate : Form
    {
        private string studentId;
        private string aId;
        public frmUpdate()
        {
            InitializeComponent();
        }
        public frmUpdate(string Message, string aid) : this()
        {
            studentId = Message;
            aId = aid;
        }
        //check valid mark(must be in 0-10)
        private bool checkValidMark()
        {
            if(!Regex.IsMatch(txtTotalScore.Text, @"^[{1}(0-9)][.][{1}(0-9)]$") &&
               !Regex.IsMatch(txtTotalScore.Text, @"^[{1}(0-9)]$") && !txtTotalScore.Text.Equals("10"))
            {
                MessageBox.Show("Wrong format score");
                return false;
            }
            return true;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkValidMark())
            {
                return;
            }
            float totalMark = float.Parse(txtTotalScore.Text.Trim());
            DAO.updateScore(totalMark, txtScoreDetail.Text, studentId);
            this.Hide();
            this.Close();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            frmLoadFile frmManage = new frmLoadFile(aId);
            frmManage.ShowDialog();
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            ArrayList sInfo = new ArrayList() { "", "", "" };
            sInfo = DAO.getStudent(sInfo,studentId);
            lbStudentId.Text = studentId;
            lbClass.Text = "Class: "+sInfo[0].ToString();
            txtScoreDetail.Text = sInfo[1].ToString();
            txtTotalScore.Text = sInfo[2].ToString();
        }
    }   
}
