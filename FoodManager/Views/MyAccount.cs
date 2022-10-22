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

namespace FoodManager.Views
{
    public partial class MyAccount : Form
    {
        private User _user = null;
        public MyAccount()
        {
            InitializeComponent();

        }

        public MyAccount(User user)
        {
            InitializeComponent();
            this._user = user;
            txbUserName.Text = _user.UserId.ToString();
            txbUserName.ReadOnly = true;
            txbDisplayname.Text = _user.FullName.ToString();
            
        }

        private bool CheckNull()
        {
            if (txbUserName.Text == "" || txbDisplayname.Text == "" || txbPass.Text==""|| txbNewPass.Text==""|| txbConfirm.Text=="")
            {
                MessageBox.Show("Vui lòng không để trống thông tin", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (!CheckNull())
            {
                return;
            }
            var username=txbUserName.Text;
            var displayname=txbDisplayname.Text;
            var pass=txbPass.Text;
            var newpass=txbNewPass.Text;
            var confirm=txbConfirm.Text;

            if (!pass.Equals(_user.Password))
            {
                MessageBox.Show("Mật khẩu không đúng. Vui lòng nhập lại!","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (!newpass.Equals(confirm))
            {

                MessageBox.Show("Mật khẩu mới không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var repo = new RepositoryBase<User>();
            User updateUser = new User();
            updateUser.UserId = username;
            updateUser.FullName = displayname;
            updateUser.Role = _user.Role;
            updateUser.Password = newpass;

            repo.Update(updateUser);
            this._user = updateUser;
            txbUserName.Text = _user.UserId.ToString();
            txbUserName.ReadOnly = true;
            txbDisplayname.Text = _user.FullName.ToString();
            txbPass.Text = "";
            txbConfirm.Text = "";
            txbNewPass.Text = "";
            MessageBox.Show("Bạn đã cập nhật thông tin cá nhân thành công","Thông báo",MessageBoxButtons.OK);

        }

        private void bntCancel_Click(object sender, EventArgs e)
        {
            txbUserName.Text = _user.UserId.ToString();
            txbUserName.ReadOnly = true;
            txbDisplayname.Text = _user.FullName.ToString();
            txbPass.Text = "";
            txbConfirm.Text = "";
            txbNewPass.Text = "";
        }
    }
}
