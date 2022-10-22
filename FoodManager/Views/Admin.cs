using Microsoft.Identity.Client;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FoodManager.Views
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            var repo = new RepositoryBase<User>();
            var listUser = repo.GetAll().Select(p => new { p.UserId, p.FullName, p.Role }).ToList();
            
            dtgvAccount.DataSource = listUser;
            var listUser1 = repo.GetAll().Select(p => new { p.Role }).ToList();
            List<string> list = new List<string>();

            for (int i = 0; i < listUser1.Count; i++)
            {
                string typeIndex = listUser1[i].Role;
                list.Add(typeIndex);

            }

            var listType = list.Distinct().ToList();


            cbAccountType.DisplayMember = "typeName";
            cbAccountType.ValueMember = "typeName";
            cbAccountType.DataSource = listType;
            btnEditAccount.Enabled = false;
            btnDeleteAccount.Enabled = false;
            btnAddAccount.Enabled = true;

        }

        private void dtgvAccount_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dtgvAccount.Rows[e.RowIndex];
            txtUserName.Text = row.Cells["UserId"].Value.ToString();

            var repo = new RepositoryBase<User>();

            txtDisplayName.Text = row.Cells["FullName"].Value.ToString();
            
            cbAccountType.Text = row.Cells["Role"].Value.ToString();
            btnEditAccount.Enabled = true;
            btnDeleteAccount.Enabled = true;
            btnAddNew.Enabled = false;



        }

        private bool CheckNull()
        {
            if (txtUserName.Text == "" || txtDisplayName.Text == "" || cbAccountType.SelectedValue.ToString() == null)
            {
                MessageBox.Show("All Input is not Null, please try again", "Notification", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            btnAddNew.Enabled = true;
            btnEditAccount.Enabled = false;
            btnDeleteAccount.Enabled = false;
            txtUserName.Text = "";
            txtDisplayName.Text = "";
            txtUserName.ReadOnly = false;


        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (!CheckNull())
            {
                return;
            }

            var username = txtUserName.Text.ToString();
            var displayname = txtDisplayName.Text.ToString();
            var type = cbAccountType.SelectedValue.ToString();

            if (username == "")
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            if (displayname == "")
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị!", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            if (type == "")
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản!", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            var repo = new RepositoryBase<User>();
            var checkUsername = repo.GetAll().Where(p => p.UserId == username).FirstOrDefault();

            if (checkUsername != null)
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại, Vùi lòng nhập tên đăng nhập khác", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            User user= new User();
            user.UserId=username.ToString();
            user.FullName = displayname.ToString();
            user.Role = type.ToString();
            user.Password = "000000";

            repo.Create(user);
            var listUser = repo.GetAll().Select(p => new { p.UserId, p.FullName, p.Role }).ToList();
            dtgvAccount.DataSource = listUser;
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (txtDisplayName.Text.ToString() == "")
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị!", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            if (cbAccountType.SelectedValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản!", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            var username = txtUserName.Text.ToString();
            var displayname = txtDisplayName.Text.ToString();
            var type = cbAccountType.SelectedValue.ToString();

            var repo = new RepositoryBase<User>();
            var user = repo.GetAll().Where(p => p.UserId == username).FirstOrDefault();

            if (user != null)
            {
                user.FullName = displayname.ToString();
                user.Role = type.ToString();
                repo.Update(user);
                var listUser = repo.GetAll().Select(p => new { p.UserId, p.FullName, p.Role }).ToList();
                dtgvAccount.DataSource = listUser;
            }
            
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {

            var username = txtUserName.Text.ToString();
            var repo = new RepositoryBase<User>();
            var user = repo.GetAll().Where(p => p.UserId == username).FirstOrDefault();

            if (user != null)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này không? ", "Xóa tài khoản", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    repo.Delete(user);
                    var listUser = repo.GetAll().Select(p => new { p.UserId, p.FullName, p.Role }).ToList();
                    dtgvAccount.DataSource = listUser;

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }

        }
    }
    }
