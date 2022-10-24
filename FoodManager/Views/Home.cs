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
            LoadCategory();
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
            LoadCategory();
        }



        #region Method
        void LoadTable()
        {
            flpTable.Controls.Clear();
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

        int GetOrderIDByTableID(int id)
        {
            var repo2 = new RepositoryBase<Order>();
            var text = repo2.GetAll().Where(p => p.TableId == id && Convert.ToInt32(p.Status) == 0).ToList();
            if (text.Count > 0)
            {
                return text.First().OrderId;
            }
            return -1;
        }

        void LoadCategory()
        {
            var repo = new RepositoryBase<Category>();
            var list = repo.GetAll().Select(p => p).ToList();
            cbCategory.DataSource = list;
            cbCategory.DisplayMember = "CateName";
        }

        void LoadFoodListByCategoryID(int id)
        {
            var repo = new RepositoryBase<Product>();
            var list = repo.GetAll().Where(p => p.CateId == id).ToList();
            cbFood.DataSource = list;
            cbFood.DisplayMember = "NameProduct";
        }

        int getMaxIdOrder()
        {
            var repo2 = new RepositoryBase<Order>();
            var text = repo2.GetAll().Select(p => p).ToList();
            int orderId = 0;
            foreach (var item in text)
            {
                orderId = item.OrderId;
            }
            return orderId;
        }

        #endregion
        #region Events
        public static int tableId;
        void btn_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            tableId = int.Parse(button.Name);
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
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;

            Category selected = cb.SelectedItem as Category;
            id = selected.CateId;
            LoadFoodListByCategoryID(id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            var repo1 = new RepositoryBase<Order>();
            var repo2 = new RepositoryBase<OrderDetail>();
            var repo3 = new RepositoryBase<Table>();
            int orderId = GetOrderIDByTableID(tableId);
            var table = repo3.GetAll().Where(p => p.TableId == tableId).FirstOrDefault();
            int foodId = (cbFood.SelectedItem as Product).ProductId;
            int quantity = (int)nmFoodCount.Value;
            DateTime date = DateTime.Now;

            if (orderId == -1)
            {
                if (quantity > 0)
                {
                    Order order = new Order();
                    order.UserId = "duynv".ToString();
                    order.TableId = tableId;
                    order.DateCheckIn = date;
                    repo1.Create(order);

                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = getMaxIdOrder();
                    orderDetail.ProductId = foodId;
                    orderDetail.Quantity = quantity;

                    repo2.Create(orderDetail);
                    table.Status = Convert.ToBoolean("True");
                    repo3.Update(table);
                }
                else
                {
                    MessageBox.Show("Không được nhập 1 số âm.", "Error", MessageBoxButtons.OK);
                    return;
                }

            }
            else
            {
                var text = repo2.GetAll().Where(p => p.OrderId == orderId && p.ProductId == foodId).FirstOrDefault();
                if (text != null)
                {
                    OrderDetail orderDetail2 = new OrderDetail();
                    //text.Quantity = quantity + text.Quantity;
                    if (quantity + text.Quantity > 0)
                    {
                        text.Quantity = quantity + text.Quantity;
                        repo2.Update(text);
                    }
                    else
                    {
                        var text3 = repo2.GetAll().Where(p => p.OrderId == orderId && p.ProductId == foodId).FirstOrDefault();
                        repo2.Delete(text3);
                    }

                }
                else
                {
                    OrderDetail orderDetail3 = new OrderDetail();
                    orderDetail3.OrderId = getMaxIdOrder();
                    orderDetail3.ProductId = foodId;
                    orderDetail3.Quantity = quantity;
                    repo2.Create(orderDetail3);
                }

            }
            ShowBill(tableId);
            LoadTable();
        }
        #endregion


    }
}
