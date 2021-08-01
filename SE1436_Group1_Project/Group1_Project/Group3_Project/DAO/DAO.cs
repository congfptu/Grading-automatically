using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Group1_Project.DAO
{
    class DAO
    {
        internal static SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["ProjectPRN292ConnectionString"].ToString());
        }

        internal static Account Login(string user, string pass)
        {
            Account acc = new Account();
            DataTable dataTable = DAO.GetDataBySQL("SELECT * FROM Account where username = '"+user+"' and password = '"+pass+"'");
            if(dataTable.Rows.Count > 0)
            {
                acc.Id = Convert.ToInt32(dataTable.Rows[0]["accountId"].ToString());
                acc.Username = dataTable.Rows[0]["username"].ToString();
                acc.Password = dataTable.Rows[0]["password"].ToString();
                return acc;
            }
            return null;
        }
        internal static bool checkAccountExist(string username)
        {
            DataTable dataTable = DAO.GetDataBySQL("select * from Account where username = '" + username + "'");
            if (dataTable.Rows.Count > 0)    
                return true;
            return false;
        }
        internal static DataTable GetDatatable(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            
            DataTable dt = new DataTable(); // database Cache
            dt.Clear();
            da.Fill(dt);
            return dt;
        }
      
        internal static DataTable GetDataBySQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet(); // database Cache
            ds.Clear();
            da.Fill(ds);
            return ds.Tables[0];


        }

        internal static void AddClass(string cid, string aid)
        {
            string sql = "INSERT INTO Class VALUES ('"+cid+"','"+aid+"')";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static DataTable getAllClassByAccId(string aid)
        {
            string sql = "select * from Class where accountID = '"+aid+"'";
            return DAO.GetDataBySQL(sql);
        }
        internal static DataTable search(string txtSearch,string className)
        {
            string sql = "select * from ScoreStudent where (StudentID like '%" + txtSearch + "%' or StudentName like '%" + txtSearch + "%' ) and className like'%" + className+"%'";
            return DAO.GetDataBySQL(sql);
        }
        internal static DataTable getExamCode()
        {
            string sql = "select * from ExamCode";
            return DAO.GetDataBySQL(sql);
        }
        internal static ArrayList getStudent(ArrayList studentInfo, string sId)
        {
            DataTable dataTable = DAO.GetDataBySQL("select className, scoreDetail, totalScore from ScoreStudent where StudentID = '" + sId + "'");
            studentInfo[0] = dataTable.Rows[0]["className"];
            studentInfo[1] = dataTable.Rows[0]["scoreDetail"];
            studentInfo[2] = dataTable.Rows[0]["totalScore"];
            return studentInfo;
        }
        internal static void insertAccount(string username, string password)
        {
            string sql = "insert into Account(username,password) values ('" + username + "','" + password + "')";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static void AddStudent(string studentId, string className, string examCode)
        {
            string sql = "insert into ScoreStudent values ('" + studentId + "','" + examCode + "','" + className + "','','','" + studentId + "')";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static void AddStudentInfo(string studentId,string className, string examCode,string studentName)
        {
            string sql = "insert into ScoreStudent values ('" + studentId + "','" + examCode + "','" + className + "','','','" + studentName + "')";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static DataTable getAllStudentByClass(string cid)
        {
            string sql = "select * from ScoreStudent where className = '" + cid + "'";
            return DAO.GetDataBySQL(sql);
        }
        internal static DataTable getAllStudent()
        {
            string sql = "select * from ScoreStudent";
            return DAO.GetDataBySQL(sql);
        }
        internal static void DeleteStudent(string classId)
        {
            string sql = "delete from ScoreStudent where className = '"+classId+"'";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static void DeleteClass(string aid)
        {
            string sql = "delete from Class where accountID = '"+aid+"'";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static List<string> listClass(string aid)
        {
            List<string> list = new List<string>();
            DataTable dataTable = DAO.GetDataBySQL("select className from Class where accountID = '"+aid+"'");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(dataRow["className"].ToString());
            }
            return list;
        }
        internal static List<string> listStudentByClass(string clName)
        {
            List<string> list = new List<string>();
            DataTable dataTable = DAO.GetDataBySQL("select StudentID from ScoreStudent where className = '" + clName + "'");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(dataRow["StudentID"].ToString());
            }
            return list;
        }

        internal static List<string> listStudentByClass1(string clName)
        {
            List<string> list = new List<string>();
            DataTable dataTable = DAO.GetDataBySQL("select StudentID+'_'+StudentName as StudentID from ScoreStudent where className = '" + clName + "'");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                list.Add(dataRow["StudentID"].ToString());
            }
            return list;
        }
        internal static void updateScore(float totalMark, string scoreDetail, string studentId)
        {
            string sql = "UPDATE ScoreStudent SET totalScore = '"+totalMark+"', scoreDetail = '"+scoreDetail+"' WHERE StudentID = '"+studentId+"'";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static void deleteClass(float totalMark, string scoreDetail, string studentId)
        {
            string sql = "delete from Class";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
        internal static void deleteScoreStudent(float totalMark, string scoreDetail, string studentId)
        {
            string sql = "delete from ScoreStudent";
            SqlCommand cmd = new SqlCommand(sql, DAO.GetConnection());
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
