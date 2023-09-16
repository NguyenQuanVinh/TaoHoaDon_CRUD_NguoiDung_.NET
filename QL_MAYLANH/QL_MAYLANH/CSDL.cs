using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QL_MAYLANH
{
    public class CSDL
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AILSKO80;Initial Catalog=TEST;Integrated Security=True");
        DataSet ds_Test = new DataSet();
        SqlDataAdapter da_user;
        SqlDataAdapter da_nhomquyen;
        SqlDataAdapter da_quyen;

        public DataTable load_user()
        {
            string CauLenh = "select * from NGUOIDUNG";

            da_user = new SqlDataAdapter(CauLenh, con);
            if (ds_Test.Tables.Contains("NGUOIDUNG"))
                ds_Test.Tables["NGUOIDUNG"].Rows.Clear();
            da_user.Fill(ds_Test, "NGUOIDUNG");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds_Test.Tables["NGUOIDUNG"].Columns[0];
            ds_Test.Tables["NGUOIDUNG"].PrimaryKey = keys;

            return ds_Test.Tables["NGUOIDUNG"];
        }

        public DataTable load_NhomQuyen()
        {
            string CauLenh = "select * from NHOMQUYEN";

            da_nhomquyen = new SqlDataAdapter(CauLenh, con);
            if (ds_Test.Tables.Contains("NHOMQUYEN"))
                ds_Test.Tables["NHOMQUYEN"].Rows.Clear();
            da_nhomquyen.Fill(ds_Test, "NHOMQUYEN");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds_Test.Tables["NHOMQUYEN"].Columns[0];
            ds_Test.Tables["NHOMQUYEN"].PrimaryKey = keys;

            return ds_Test.Tables["NHOMQUYEN"];
        }

        public DataTable load_Quyen()
        {
            string CauLenh = "select * from QUYEN";

            da_quyen = new SqlDataAdapter(CauLenh, con);
            if (ds_Test.Tables.Contains("QUYEN"))
                ds_Test.Tables["QUYEN"].Rows.Clear();
            da_quyen.Fill(ds_Test, "QUYEN");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds_Test.Tables["QUYEN"].Columns[0];
            ds_Test.Tables["QUYEN"].PrimaryKey = keys;

            return ds_Test.Tables["QUYEN"];
        }

        public void Luu()
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_user);
            da_user.Update(ds_Test, "NGUOIDUNG");
        }

        public void Xoa(string pID)
        {
            DataRow row = ds_Test.Tables["NGUOIDUNG"].Rows.Find(pID);
            if (row != null)
                row.Delete();
            string CauLenh = "select * from NGUOIDUNG";
            SqlDataAdapter da = new SqlDataAdapter(CauLenh, con);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.Update(ds_Test, "NGUOIDUNG");
        }

        public string getID(string username, string pass)
        {
            string id = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOIDUNG WHERE TENTAIKHOAN ='" + username + "' and MATKHAU='" + pass + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        id = dr["IDUSER"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                con.Close();
            }
            return id;
        }

        public string getQH(string IDUSER)
        {
            string qh = "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IDNHOMQUYEN FROM NGUOIDUNG WHERE IDUSER = '"+IDUSER+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        qh = dr["IDNHOMQUYEN"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi xảy ra khi truy vấn dữ liệu hoặc kết nối với server thất bại !");
            }
            finally
            {
                con.Close();
            }
            qh=qh.Trim();
            return qh;
        }
    }
}
