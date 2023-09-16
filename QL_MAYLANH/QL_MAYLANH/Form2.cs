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
    public partial class Form2 : Form
    {
        CSDL dt = new CSDL();
        public Form2()
        {
            InitializeComponent();
        }
        public static string ID_USER = "";
        public static string QUYENHAN = "";
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            ID_USER = dt.getID(txt_username.Text, txt_pass.Text);
            if (ID_USER != "")
            {

                QUYENHAN = dt.getQH(ID_USER);
                if(QUYENHAN == "QT" )
                {
                    MessageBox.Show("Ban la quan tri");
                    Form3 fmain = new Form3(QUYENHAN);
                    fmain.Show();
                    this.Hide();
                }
                else if(QUYENHAN == "NV")
                {
                    MessageBox.Show("Ban la nhan vien");
                    Form3 fmain = new Form3(QUYENHAN);
                    fmain.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Ban la ai");
                }
            }
            else
            {
                MessageBox.Show("Tài khoản và mật khẩu không đúng !");
            }
            
        }
    }
}
