using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace QL_MAYLANH
{
    public class Data
    {
        public Data() { }

        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AILSKO80;Initial Catalog=QL_MAYLANH;Integrated Security=True");
        DataSet ds_QLMAYLANH = new DataSet();
        SqlDataAdapter da_SP;
        SqlDataAdapter da_KH;
        SqlDataAdapter da_CTHD;
        SqlDataAdapter da_HD;

        public DataTable load_SP()
        {
            string CauLenh = "select * from SANPHAM";

            da_SP = new SqlDataAdapter(CauLenh, con);

            da_SP.Fill(ds_QLMAYLANH, "SANPHAM");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds_QLMAYLANH.Tables["SANPHAM"].Columns[0];
            ds_QLMAYLANH.Tables["SANPHAM"].PrimaryKey = keys;

            return ds_QLMAYLANH.Tables["SANPHAM"];
        }

        public DataTable load_KH()
        {
            string CauLenh = "select * from KHACHHANG";

            da_KH = new SqlDataAdapter(CauLenh, con);
            if (ds_QLMAYLANH.Tables.Contains("KHACHHANG"))
                ds_QLMAYLANH.Tables["KHACHHANG"].Rows.Clear();
            da_KH.Fill(ds_QLMAYLANH, "KHACHHANG");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds_QLMAYLANH.Tables["KHACHHANG"].Columns[0];
            ds_QLMAYLANH.Tables["KHACHHANG"].PrimaryKey = keys;

            return ds_QLMAYLANH.Tables["KHACHHANG"];
        }

        public DataTable load_HD()
        {
            string CauLenh = "select * from HOADONKH";

            da_HD = new SqlDataAdapter(CauLenh, con);
            if (ds_QLMAYLANH.Tables.Contains("HOADON"))
                ds_QLMAYLANH.Tables["HOADON"].Rows.Clear();
            da_HD.Fill(ds_QLMAYLANH, "HOADON");

            DataColumn[] keys = new DataColumn[1];
            keys[0] = ds_QLMAYLANH.Tables["HOADON"].Columns[0];
            ds_QLMAYLANH.Tables["HOADON"].PrimaryKey = keys;

            return ds_QLMAYLANH.Tables["HOADON"];
        }

        public DataTable load_CTHD()
        {
            string CauLenh = "select * from CHITIETHOADONKH";

            da_CTHD = new SqlDataAdapter(CauLenh, con);

            if (ds_QLMAYLANH.Tables.Contains("CTHD"))
                ds_QLMAYLANH.Tables["CTHD"].Rows.Clear();
            da_CTHD.Fill(ds_QLMAYLANH, "CTHD");

            DataColumn[] keys = new DataColumn[2];
            keys[0] = ds_QLMAYLANH.Tables["CTHD"].Columns[0];
            keys[1] = ds_QLMAYLANH.Tables["CTHD"].Columns[1];
            ds_QLMAYLANH.Tables["CTHD"].PrimaryKey = keys;

            return ds_QLMAYLANH.Tables["CTHD"];
        }

        public bool ThemKH(string pMaKH, string pTenKH, string pSDT, string pDiaChi)
        {
            try
            {
                DataRow dong = ds_QLMAYLANH.Tables["KHACHHANG"].NewRow();
                dong[0] = pMaKH;
                dong[1] = pTenKH;
                dong[2] = pSDT;
                dong[3] = pDiaChi;

                ds_QLMAYLANH.Tables["KHACHHANG"].Rows.Add(dong);
                //SqlCommandBuilder cmb = new SqlCommandBuilder(da_KH);
                //da_KH.Update(ds_QLMAYLANH, "KHACHHANG");

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ThemHD(string pMaHD, string pMaKH, string pNgay, string pTongTien, string pTongSL)
        {
            try
            {
                DataRow dong = ds_QLMAYLANH.Tables["HOADON"].NewRow();
                dong[0] = pMaHD;
                dong[1] = pMaKH;
                dong[2] = pNgay;
                dong[3] = pTongTien;
                dong[4] = pTongSL;

                ds_QLMAYLANH.Tables["HOADON"].Rows.Add(dong);
                //SqlCommandBuilder cmb = new SqlCommandBuilder(da_HD);
                //da_HD.Update(ds_QLMAYLANH, "HOADON");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ThemCTHD(string pMaHD, string pMaSP, string pSoLg)
        {
            try
            {
                DataRow dong = ds_QLMAYLANH.Tables["CTHD"].NewRow();
                dong[0] = pMaHD;
                dong[1] = pMaSP;
                dong[2] = pSoLg;
                ds_QLMAYLANH.Tables["CTHD"].Rows.Add(dong);
                //SqlCommandBuilder cmb = new SqlCommandBuilder(da_CTHD);
                //da_CTHD.Update(ds_QLMAYLANH, "CTHD");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SuaHD(string pMaHD, string pTongTien, string pTongSL)
        {
            DataRow dong = ds_QLMAYLANH.Tables["HOADON"].Rows.Find(pMaHD);
            if (dong != null)
            {
                dong[2] = pTongTien;
                dong[3] = pTongSL;
            }

        }
        public void XacNhan()
        {
            SqlCommandBuilder cmb = new SqlCommandBuilder(da_KH);
            da_KH.Update(ds_QLMAYLANH, "KHACHHANG");
            SqlCommandBuilder cmb2 = new SqlCommandBuilder(da_HD);
            da_HD.Update(ds_QLMAYLANH, "HOADON");
            SqlCommandBuilder cmb3 = new SqlCommandBuilder(da_CTHD);
            da_CTHD.Update(ds_QLMAYLANH, "CTHD");
        }

        public string maxMaHD()
        {
            DataRow row = load_HD().Rows[0];
            if (row == null)
                return null;
            foreach (DataRow dr in ds_QLMAYLANH.Tables["HOADON"].Rows)
                if (int.Parse(row[0].ToString().Substring(3)) < int.Parse(dr[0].ToString().Substring(3)))
                    row = dr;
            return row[0].ToString();
        }

        public string maxMaKH()
        {
            DataRow row = load_KH().Rows[0];
            if (row == null)
                return null;
            foreach (DataRow dr in ds_QLMAYLANH.Tables["KHACHHANG"].Rows)
                if (int.Parse(row[0].ToString().Substring(3)) < int.Parse(dr[0].ToString().Substring(3)))
                    row = dr;
            return row[0].ToString();
        }

    }
}
