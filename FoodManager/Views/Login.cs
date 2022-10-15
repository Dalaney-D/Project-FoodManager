using FoodManager.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodManager
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void bntExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        bool bLogin(string username, string pass)
        {
            return AccountDAO.Instance.bLogin(username, pass);
        }

        private void bntLogin_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string pass = txbPass.Text;
            if (bLogin(username, pass))
            {
                Home f = new Home();
                this.Hide();
                f.ShowDialog();
                f.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu ");
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
