using Group1_Project.DAO;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmLoadFile : Form
    {
        private string aId;
        public frmLoadFile()
        {
            InitializeComponent();
            comboBox1.DataSource = DAO.getExamCode();
            comboBox1.DisplayMember = "examCode";
            comboBox1.ValueMember = "examCode";
        }
        public frmLoadFile(string Message) : this()
        {
            aId = Message;
        }
       //Load class into Combobox
        private void LoadComboBox()
        {
            DataTable listClass = DAO.getAllClassByAccId(aId);
            if(listClass.Rows.Count > 0)
            {
                DataRow dr = listClass.NewRow();
                dr["ClassName"] = "-----All-----";
                listClass.Rows.InsertAt(dr, 0);

                this.cbbClass.DataSource = listClass;
                cbbClass.DisplayMember = "ClassName";
                cbbClass.ValueMember = "ClassName";
                cbbClass.Enabled = true;
            }
        }
        //Load infomation of student into datagridview
        private void LoadDgvStudent()
        {
            dgvStudent.Columns.Clear();
            if (cbbClass.SelectedValue != null)
            {
                if(cbbClass.SelectedIndex == 0)
                {
                    dgvStudent.DataSource = DAO.getAllStudent();
                    setHeaderTextDgv();
                }
                else
                {
                    dgvStudent.DataSource = DAO.getAllStudentByClass(cbbClass.SelectedValue.ToString());
                    setHeaderTextDgv();
                }
                addEdit();
            }
            dgvStudent.Enabled = true;          
        }
        private void setHeaderTextDgv()
        {
            for (int i = 0; i < dgvStudent.Columns.Count; i++)
            {
                dgvStudent.Columns[i].HeaderText = dgvStudent.Columns[i].HeaderText.ToUpper();
            }
        }
        private void addEdit()
        {
            DataGridViewButtonColumn btnColumnEdit = new DataGridViewButtonColumn();
            btnColumnEdit.Name = "editColumn";
            btnColumnEdit.HeaderText = "EDIT";
            btnColumnEdit.Text = "Edit";
            btnColumnEdit.UseColumnTextForButtonValue = true;
            dgvStudent.Columns.Add(btnColumnEdit);
        }

        private void FrmManage_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDgvStudent();
            if(dgvStudent.DataSource != null && cbbClass.DataSource != null)
            {
                btnGenerate.Text = "Re-Generate";
                txtGen.Text = "";
                txtGen.Enabled = false;
            }
            else
            {
                btnGenerate.Text = "Generate";
                txtGen.Enabled = true;
                cbbClass.Enabled = false;
                dgvStudent.Enabled = false;
                btnAutoMark.Enabled = false;
                btnExport.Enabled = false;
                btnSearch.Enabled = false;
                txtSearch.Enabled = false;
            }
        }
        //generate data of file class_test
        private void btnGenerate_Click(object sender, EventArgs e)
        {

            if (txtGen.Text.Trim().Length == 0 && txtGen.Enabled == true)
            {
                txtGen.Text = "";
                return;
            }
            if (btnGenerate.Text.Equals("Re-Generate"))
            {
                List<string> listClass = DAO.listClass(aId);
                foreach(string cl in listClass)
                {
                    DAO.DeleteStudent(cl);
                }
                DAO.DeleteClass(aId);
                btnGenerate.Text = "Generate";
                txtGen.Enabled = true;
                cbbClass.DataSource = null;
                cbbClass.Enabled = false;
                dgvStudent.DataSource = null;
                dgvStudent.Enabled = false;
                btnAutoMark.Enabled = false;
                btnExport.Enabled = false;
                btnSearch.Enabled = false;
                txtSearch.Enabled = false;
                dgvStudent.Columns.Clear();
            }
            else
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine(@"cd \");
                cmd.StandardInput.WriteLine(@"cd /d " + txtGen.Text);
                cmd.StandardInput.WriteLine("dir");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                string txt = cmd.StandardOutput.ReadToEnd();
                if (!txt.Contains("SE"))
                {
                    MessageBox.Show("Invalid Folder Class");
                    return;
                }
                string[] list = txt.Split('\n');
                string classId = "";
                txt = "";
              
                
                    foreach (string item in list)
                    {
                        if (item.Contains("DIR") && !item.Contains("."))
                        {
                            classId = item.Substring(item.Length - 7, 6);
                            DAO.AddClass(classId, aId);
                            txt += classId + "\n";
                            classId = "";
                        }
                    }             
                string[] listClass = txt.Split('\n');
                txt = "";
                for(int i =0; i<listClass.Length-1;i++)
                {
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.Start();
                    cmd.StandardInput.WriteLine(@"cd \");
                    cmd.StandardInput.WriteLine(@"cd /d " + txtGen.Text+@"\"+ listClass[i]);
                    cmd.StandardInput.WriteLine("dir");
                    cmd.StandardInput.Flush();
                    cmd.StandardInput.Close();
                    cmd.WaitForExit();
                    txt = cmd.StandardOutput.ReadToEnd();
                    string[] listSt = txt.Split('\n');
                    string studentId = "";
                    string studentName = "";
                    foreach (string item in listSt)
                    {
                        if (item.Contains("DIR") && !item.Contains("."))
                        {
                            try
                            {
                                int index = item.IndexOf("_");
                                studentName = item.Substring(index+1);
                                studentId = item.Substring(index- 8, 8);
                                DAO.AddStudentInfo(studentId, listClass[i], comboBox1.Text,studentName);
                                //label1.Text += studentId + "\n";
                                studentId = "";
                            }
                            catch
                            {
                               
                            }
                        }
                    }
                }
                btnGenerate.Text = "Re-Generate";
                txtGen.Text = "";
                txtGen.Enabled = false;
                LoadComboBox();
                LoadDgvStudent();
                btnAutoMark.Enabled = true;
                btnExport.Enabled = true;
                txtSearch.Enabled = true;
                btnSearch.Enabled = true;
            }
        }

        private void cbbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDgvStudent();
        }

        private void txtGen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtGen.Text = dialog.SelectedPath;
        }
        //export to excel
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvStudent.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass XcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                XcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvStudent.Columns.Count; i++)
                {
                    XcelApp.Cells[1, i] = dgvStudent.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvStudent.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvStudent.Columns.Count-1; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = dgvStudent.Rows[i].Cells[j].Value;
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }

        private void btnAutoMark_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAutoMark autoMark = new frmAutoMark(aId);
            autoMark.ShowDialog();
            this.Close();
        }
        //edit info
        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvStudent.CurrentRow.Index;
            if (dgvStudent.Rows[i].Cells[6].Selected == true)
            {
                frmUpdate frmUpdate = new frmUpdate(dgvStudent.Rows[i].Cells[0].Value.ToString(), aId);
                this.Hide();
                frmUpdate.ShowDialog();
                this.Close();
            }
        }
        //log out
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            new frmLogin().ShowDialog();
            this.Close();
        }

        private void cbbClass_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text.Trim().Length == 0)
            {
                txtSearch.Text = "";
            }
            if(cbbClass.Text.Equals("-----All-----"))
            dgvStudent.DataSource = DAO.search(txtSearch.Text.Trim(), "");
            else
            dgvStudent.DataSource = DAO.search(txtSearch.Text.Trim(),cbbClass.Text);
            setHeaderTextDgv();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
