
using Repository;
using Repository.Models;
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
        public static User _user=new User();
        public Login()
        {
            InitializeComponent();
            txbUserName.Text = "";
            txbPass.Text = "";

        }

        private void bntExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void bntLogin_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string pass = txbPass.Text;

            if (username == "")
            {
                MessageBox.Show("Tên đăng nhập không thể để trống. Vui Lòng nhập tên đăng nhập");
            } else if (pass == "")
            {
                MessageBox.Show("Mật khẩu không thể để trống. Vui Lòng nhập mật khẩu");
            }
            else {
                var repo = new RepositoryBase<User>();
                var user= repo.GetAll().Where(p=>p.UserId == username && p.Password==pass).FirstOrDefault();
                if(user!=null){
                    _user=user;

                    Home f = new Home(user);
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu ");
                }
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
