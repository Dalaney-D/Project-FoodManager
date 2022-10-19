using FoodManager.Views;
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
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Profile f = new Profile();
            f.ShowDialog();
        }
        
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.ShowDialog(); 
        }
    }
}
