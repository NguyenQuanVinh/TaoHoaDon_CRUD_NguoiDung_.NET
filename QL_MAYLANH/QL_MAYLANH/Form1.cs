using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_MAYLANH
{
    public partial class Form1 : Form
    {
        Data dt = new Data();
        public Form1()
        {
            InitializeComponent();
        }

        public void load_SP()
        {
            DataTable tb = dt.load_SP();
            cboSanPham.DataSource = tb;
            cboSanPham.DisplayMember = "TENSP";
            cboSanPham.ValueMember = "MASP";

            txtDonGia.DataBindings.Add("Text", tb, "DONGIA");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //load du lieu tu database
            load_SP();
            //
            btnThem.Enabled = false;
            cboSanPham.SelectedIndex = -1;
            txtDonGia.Clear();
            //Trang thai dgv
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == string.Empty)
            {
                txtGiaBan.Text = "0";
                btnThem.Enabled = false;
            }
            else
            {
                
                int gb = int.Parse(txtSoLuong.Text) * int.Parse(txtDonGia.Text);
                txtGiaBan.Text = gb.ToString();
                btnThem.Enabled = true;
            }
        }
        //cap nhat tong so luong va tong tien
        public int capNhatSL()
        {
            int sl = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                sl += int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            return sl;
        }
        public int capNhatTC()
        {
            int tc = 0;
            for(int i=0;i<dataGridView1.Rows.Count;i++)
            {
                tc += int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()) * int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            return tc;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(cboSanPham.SelectedValue.ToString(), txtDonGia.Text, txtSoLuong.Text);
            btnThem.Enabled = false;
            txtTongCong.Text = capNhatTC().ToString();  
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSoLuong.Clear();
            txtGiaBan.Clear();
        }

        private void txtTienCoc_Leave(object sender, EventArgs e)
        {
            if (txtTienCoc.Text != string.Empty)
            {
                txtTongCong.Text = capNhatTC().ToString();
                int temp = int.Parse(txtTongCong.Text) - int.Parse(txtTienCoc.Text);
                txtTongCong.Text = temp.ToString();
            }
            else
                txtTongCong.Text = capNhatTC().ToString();
        }

        public void KhoiTao()
        {
            dataGridView1.Rows.Clear();
            txtTenKH.Clear();
            txtDT.Clear();
            txtSoLuong.Clear();
            txtDiaChi.Clear();
            txtTienCoc.Clear();
            txtTongCong.Clear();
            cboSanPham.SelectedIndex = -1;
            txtDonGia.Clear();
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn làm mới?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(r == DialogResult.Yes)
            {
                KhoiTao();
            }

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tb_KH = dt.load_KH();
                DataTable tb_HD = dt.load_HD();
                DataTable tb_CTHD = dt.load_CTHD();

                string name0 = dt.maxMaKH();
                string maKH;
                if (name0 == null)
                    maKH = "KH-0";
                else
                    maKH = "KH-" + (int.Parse(name0.Substring(3)) + 1).ToString();

                string name = dt.maxMaHD();
                string maHD;
                if (name == null)
                    maHD = "HD-0";
                else
                    maHD = "HD-" + (int.Parse(name.Substring(3)) + 1).ToString();

                if (tb_KH.Rows.Find(maKH) == null)
                {
                    dt.ThemKH(maKH, txtTenKH.Text, txtDT.Text, txtDiaChi.Text);
                }
                if (tb_HD.Rows.Find(maHD) == null)
                {
                    dt.ThemHD(maHD, maKH, DateTime.Now.ToShortDateString(), capNhatTC().ToString(), capNhatSL().ToString());
                }
                for(int i =0;i<dataGridView1.Rows.Count;i++)
                {
                    string maSP = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string sl = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    dt.ThemCTHD(maHD, maSP, sl);
                }
                dt.XacNhan();
                MessageBox.Show("TẠO ĐƠN HÀNG THÀNH CÔNG", "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KhoiTao();
            }
            catch
            {
                MessageBox.Show("That Bai");
            }
        }

        //Xoa va cap nhat lai tong tien
        private void xóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            txtTongCong.Text = capNhatTC().ToString();
        }
    }
}
