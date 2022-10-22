using FoodManager.Views;
using Microsoft.EntityFrameworkCore;
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
        private User _user = null;
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

        public Home(User user)
        {

            InitializeComponent();
            this._user = user;
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
            var list = repo.GetAll().Select(p => new { p.TableId, p.Status }).ToList();
            foreach (var item in list)
            {
                Button btn = new Button() { Width = 100, Height = 100 };
                btn.Text = "Bàn " + item.TableId + Environment.NewLine + Convert.ToString(item.Status ? "Có khách" : "Trống");
                //btn.Text = item.TableId + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Name = item.TableId.ToString();

                if (item.Status)
                {
                    btn.BackColor = Color.LightPink;
                }
                else
                {
                    btn.BackColor = Color.Aqua;
                }
                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            {
                lsvBill.Items.Clear();
                var repo2 = new RepositoryBase<OrderDetail>();
                var text = repo2.GetAll2().Include(p => p.Order).Include(p => p.Product).Where(p => p.Order.TableId.Equals(id) && Convert.ToInt32(p.Order.Status) == 0).ToList();

                //var bill = text.Select(p => new
                //{
                //    FoodName = p.Product.NameProduct,
                //    Quantity = p.Quantity,
                //    Price = p.Product.Price,
                //    TotalPrice = p.Quantity * p.Product.Price
                //}).ToList();
                //dtgv1.DataSource = bill;

                double totalPrice = 0;
                foreach (var item in text)
                {
                    ListViewItem lsvItem = new ListViewItem(item.Product.NameProduct.ToString());
                    lsvItem.SubItems.Add(item.Quantity.ToString());
                    lsvItem.SubItems.Add(item.Product.Price.ToString());
                    lsvItem.SubItems.Add((item.Quantity * item.Product.Price).ToString());
                    totalPrice += (item.Quantity * item.Product.Price);
                    lsvBill.Items.Add(lsvItem);
                }
                txbTotalPrice.Text = totalPrice.ToString("c");
            }
        }
        #endregion
        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int tableId = int.Parse(button.Name);
            ShowBill(tableId);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyAccount f = new MyAccount(_user);

            f.ShowDialog();
            var repo = new RepositoryBase<User>();
            var newUser = repo.GetAll().Where(p => p.UserId.Equals(_user.UserId)).FirstOrDefault();
            this._user = newUser;
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
