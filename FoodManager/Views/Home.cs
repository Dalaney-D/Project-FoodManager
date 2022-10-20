using FoodManager.Views;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodManager
{
    public partial class Home : Form
    {
        public static User _user=Login._user;
        public Home()
        {
            InitializeComponent();
            if (!_user.Role.Equals("Admin"))
            {
                adminToolStripMenuItem.Enabled = false;
            }

            //var repo = new RepositoryBase<User>();
            //var list = repo.GetAll().Select(p => new { p.UserId, p.Role }).ToList();
            //dtgvTest.DataSource = list;
            LoadTable();

        }

        #region Method
        void LoadTable()
        {
            var repo = new RepositoryBase<Table>();
            var list = repo.GetAll().Select(p => new {p.TableId, p.Status}).ToList();
            foreach (var item in list)
            {
                Button btn = new Button() { Width = 100, Height = 100};
                btn.Text = "Bàn " + item.TableId + Environment.NewLine + Convert.ToString(item.Status?"Có khách":"Trống");

                if (item.Status)
                {
                    btn.BackColor = Color.LightPink;
                }
                else {
                    btn.BackColor = Color.Aqua;
                }
                flpTable.Controls.Add(btn);
            }
        }
        #endregion
        #region Events
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile f = new Profile();
            f.ShowDialog();
        }
        

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin a = new Admin();
            a.ShowDialog();
        }
        #endregion
    }
}
