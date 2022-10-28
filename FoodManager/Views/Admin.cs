using Microsoft.EntityFrameworkCore;
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
        public static User _user = Login._user;
        public Admin()
        {
            if (_user.Role.Equals("Admin"))
            {
                InitializeComponent();
                loadDataCate();
                loadDataTable();
                loadListBill();
            }
        }

        #region methods Category
        void loadDataCate()
        {
            var CategoryRepo = new RepositoryBase<Category>();
            var listCategory = CategoryRepo.GetAll().Select(e => new { e.CateId, e.CateName }).ToList();
            dtgvCategory.DataSource = listCategory;
        }

        void ResetFormCate()
        {
            txtCategoryID.Text = "";
            txtNameCategory.Text = "";

            btnDeleteCategory.Enabled = false;
            btnEditCategory.Enabled = false;
            btnAddCategory.Enabled = true;
            btnSearchCategory.Enabled = true;
        }

        void searchCate()
        {
            var CategoryRepo = new RepositoryBase<Category>();
            var listCategory = CategoryRepo.GetAll().Select(e => new { e.CateId, e.CateName }).Where(e => e.CateName.Contains(txtSearchCategory.Text)).ToList();
            dtgvCategory.DataSource = listCategory;
            ResetFormCate();
        }

        bool CheckNullCateName()
        {
            if (txtNameCategory.Text == "")
            {
                MessageBox.Show("Tên danh mục không phải là Null, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        void addCate()
        {
            if (!CheckNullCateName())
            {
                return;
            }
            var CateName = txtNameCategory.Text.ToString();
            var CategoryRepo = new RepositoryBase<Category>();
            var Category = new Category();
            Category.CateName = CateName;
            CategoryRepo.Create(Category);
            var ListCategory = CategoryRepo.GetAll().Select(e => new { e.CateId, e.CateName }).ToList();
            dtgvCategory.DataSource = ListCategory;
            ResetFormCate();
        }

        void deleteCate()
        {
            var ID = txtCategoryID.Text;
            var CategoryRepo = new RepositoryBase<Category>();
            var obj = CategoryRepo.GetAll().Where(e => e.CateId.ToString().Equals(ID)).FirstOrDefault();
            if (obj != null)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa không ? ", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    CategoryRepo.Delete(obj);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }
            var listCategory = CategoryRepo.GetAll().Select(e => new { e.CateId, e.CateName }).ToList();
            dtgvCategory.DataSource = listCategory;
            ResetFormCate();
        }

        bool CheckNullCate()
        {
            if (txtCategoryID.Text == "" || txtNameCategory.Text == "")
            {
                MessageBox.Show("Tất cả đầu vào không phải là Null, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        void editCate()
        {
            if (!CheckNullCate())
            {
                return;
            }
            var CateID = txtCategoryID.Text;
            var CateName = txtNameCategory.Text;
            var CategoryRepo = new RepositoryBase<Category>();
            var cate = CategoryRepo.GetAll().Where(e => e.CateId.ToString().Equals(CateID)).FirstOrDefault();
            if (cate != null)
            {
                cate.CateName = CateName;
                CategoryRepo.Update(cate);
            }
            var listCate = CategoryRepo.GetAll().Select(e => new { e.CateId, e.CateName }).ToList();
            dtgvCategory.DataSource = listCate;
            ResetFormCate();
        }
        #endregion

        #region events Category
        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            ResetFormCate();
            loadDataCate();
        }

        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCategoryID.Enabled = false;
                var rowSelected = this.dtgvCategory.Rows[e.RowIndex];
                txtCategoryID.Text = rowSelected.Cells["CateId"].Value.ToString();
                txtNameCategory.Text = rowSelected.Cells["CateName"].Value.ToString();
            }
            btnAddCategory.Enabled = false;
            btnDeleteCategory.Enabled = true;
            btnEditCategory.Enabled = true;
            btnSearchCategory.Enabled = true;
            btnShowCategory.Enabled = true;
        }

        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            searchCate();
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            addCate();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            deleteCate();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            editCate();
        }
        #endregion

        #region methods Table

        void loadDataTable()
        {
            var TableRepo = new RepositoryBase<Table>();
            var listTable = TableRepo.GetAll().Select(e => new { e.TableId, e.TableName }).ToList();
            dtgvTable.DataSource = listTable;
        }

        void ResetFormTable()
        {
            txtIDTable.Text = "";
            txtTableName.Text = "";
            btnDeleteTable.Enabled = false;
            btnEditTable.Enabled = false;
            btnAddTable.Enabled = true;
            btnSearchTable.Enabled = true;
        }

        void searchTable()
        {
            var TableRepo = new RepositoryBase<Table>();
            var listTable = TableRepo.GetAll().Select(e => new { e.TableId, e.TableName }).Where(e => e.TableName.Contains(txtSearchTable.Text)).ToList();
            dtgvTable.DataSource = listTable;
            ResetFormTable();
        }

        bool CheckNullTableName()
        {
            if (txtTableName.Text == "")
            {
                MessageBox.Show("Tên bàn không phải là Null, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        void addTable()
        {
            if (!CheckNullTableName())
            {
                return;
            }
            var TableName = txtTableName.Text.ToString();
            var TableRepo = new RepositoryBase<Table>();
            var Table = new Table();
            Table.TableName = TableName;
            TableRepo.Create(Table);
            var ListTable = TableRepo.GetAll().Select(e => new { e.TableId, e.TableName }).ToList();
            dtgvTable.DataSource = ListTable;
            ResetFormTable();
        }

        void deleteTable()
        {
            var ID = txtIDTable.Text;
            var TableRepo = new RepositoryBase<Table>();
            var obj = TableRepo.GetAll().Where(e => e.TableId.ToString().Equals(ID)).FirstOrDefault();
            if (obj != null)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa không ? ", "Thông báo", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if(obj.Status) 
                    {
                        MessageBox.Show("Bàn ăn đang có người, không thể xóa !", "Thông báo", MessageBoxButtons.OK);
                    }
                    else 
                    {
                        TableRepo.Delete(obj);
                    }                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }
            var listTable = TableRepo.GetAll().Select(e => new { e.TableId, e.TableName }).ToList();
            dtgvTable.DataSource = listTable;
            ResetFormTable();
        }

        bool CheckNullTable()
        {
            if (txtIDTable.Text == "" || txtTableName.Text == "")
            {
                MessageBox.Show("Tất cả đầu vào không phải là Null, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        void editTable()
        {
            if (!CheckNullTable())
            {
                return;
            }
            var TableID = txtIDTable.Text;
            var TableName = txtTableName.Text;
            var TableRepo = new RepositoryBase<Table>();
            var table = TableRepo.GetAll().Where(e => e.TableId.ToString().Equals(TableID)).FirstOrDefault();
            if (table != null)
            {
                table.TableName = TableName;
                TableRepo.Update(table);
            }
            var listTable = TableRepo.GetAll().Select(e => new { e.TableId, e.TableName }).ToList();
            dtgvTable.DataSource = listTable;
            ResetFormTable();
        }
        #endregion

        #region events Table
        private void btnSearchTable_Click(object sender, EventArgs e)
        {
            searchTable();
            ResetFormTable();
        }

        private void dtgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var rowSelected = this.dtgvTable.Rows[e.RowIndex];
                txtIDTable.Text = rowSelected.Cells["TableId"].Value.ToString();
                txtTableName.Text = rowSelected.Cells["TableName"].Value.ToString();
            }
            btnAddTable.Enabled = false;
            btnDeleteTable.Enabled = true;
            btnEditTable.Enabled = true;
            btnSearchTable.Enabled = true;
            btnShowTable.Enabled = true;
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            addTable();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            deleteTable();
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            editTable();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            ResetFormTable();
            loadDataTable();
        }
        #endregion

        #region methods turnover
        
        void loadListBill(DateTime checkIn, DateTime checkOut)
        {                  
            var OrderRepo = new RepositoryBase<Order>();            
            var listBill = OrderRepo.GetAll().Where(e => e.DateCheckOut >= checkIn && e.DateCheckOut <= checkOut && Convert.ToInt32(e.Status) == 1).Select(e => new { e.OrderId, e.UserId, e.TableId, e.DateCheckIn, e.DateCheckOut, e.Total}).ToList();
            dtgvBill.DataSource = listBill;
            dtgvBill.Columns[5].ToString();
            dtgvBill.Columns[5].DefaultCellStyle.Format = "00,0 vnđ";
            int totalPrice = 0;
            foreach (var item in listBill)
            {
                totalPrice += Convert.ToInt32(item.Total);
            }
            txtTotal.Text = String.Format("{0:0,0 vnđ}", totalPrice);

        }

        void loadListBill()
        {
            var OrderRepo = new RepositoryBase<Order>();
            var listBill = OrderRepo.GetAll().Where(e => Convert.ToInt32(e.Status) == 1).Select(e => new { e.OrderId, e.UserId, e.TableId, e.DateCheckIn, e.DateCheckOut, e.Total}).ToList();
            dtgvBill.DataSource = listBill;
            dtgvBill.Columns[5].ToString();
            dtgvBill.Columns[5].DefaultCellStyle.Format = "00,0 vnđ";
            int totalPrice = 0;
            foreach (var item in listBill)
            {
                totalPrice += Convert.ToInt32(item.Total);               
            }
            txtTotal.Text = String.Format("{0:0,0 vnđ}", totalPrice);
        }
       
        #endregion

        #region events turnover

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            loadListBill(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        #endregion


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
            ResetFormAccount();

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
            txtUserName.ReadOnly = true;



        }

        private bool CheckNull()
        {
            if (txtUserName.Text == "" || txtDisplayName.Text == "" || cbAccountType.SelectedValue.ToString() == null)
            {
                MessageBox.Show("Vui lòng không để trống thông tin", "Thông báo", MessageBoxButtons.OK);
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


            var repo = new RepositoryBase<User>();
            var checkUsername = repo.GetAll().Where(p => p.UserId == username).FirstOrDefault();

            if (checkUsername != null)
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại, Vui lòng nhập tên đăng nhập khác", "Error", MessageBoxButtons.OK);
                btnEditAccount.Enabled = false;
                btnDeleteAccount.Enabled = false;
                return;
            }

            User user = new User();
            user.UserId = username.ToString();
            user.FullName = displayname.ToString();
            user.Role = type.ToString();
            user.Password = "000000";

            repo.Create(user);
            var listUser = repo.GetAll().Select(p => new { p.UserId, p.FullName, p.Role }).ToList();
            dtgvAccount.DataSource = listUser;
            ResetFormAccount();
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            if (!CheckNull())
            {
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
                ResetFormAccount();
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
                    ResetFormAccount();

                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
            }

        }

        private void ResetFormAccount()
        {
            txtUserName.ReadOnly = false;
            txtUserName.Text = "";
            txtDisplayName.Text = "";
            cbAccountType.SelectedIndex = 0;

            btnAddNew.Enabled = true;
            btnEditAccount.Enabled = false;
            btnDeleteAccount.Enabled = false;
        }

        private void btnCancelChange_Click(object sender, EventArgs e)
        {
            ResetFormAccount();
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            btnAddFood.Enabled = true;
            var FoodRepo = new RepositoryBase<Product>();
            var listFood = FoodRepo.GetAll().ToList();
            var item2 = FoodRepo.GetAll2().Include(p => p.Cate).Select(p => new { p.ProductId, p.NameProduct, p.Price, p.CateId, p.Cate.CateName }).ToList();

            var CateRepo = new RepositoryBase<Category>();
            var listCategory = CateRepo.GetAll().ToList().Select(p => new { CateId = p.CateId, Name = p.CateName }).ToList();
            cbFoodCategory.ValueMember = "CateId";
            cbFoodCategory.DisplayMember = "Name";
            cbFoodCategory.DataSource = (listCategory.ToArray());
            dtgvFood.DataSource = item2;
            ResetFormFood();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (!CheckFoodNull())
            {
                return;
            }
            btnAddFood.Enabled = false;
            var FoodRepo = new RepositoryBase<Product>();

            var ProductId = int.Parse(txtFoodID.Text);
            var FoodName = txtFoodName.Text;
            var Price = double.Parse(mnFoodPrice.Value.ToString());
            var CateId = int.Parse(cbFoodCategory.SelectedValue.ToString());
            var CheckId = FoodRepo.GetAll().Where(p => p.ProductId==ProductId).FirstOrDefault();
            if (ProductId == null)
            {
                MessageBox.Show("Please Input ProductId", "Error", MessageBoxButtons.OK);
                btnAddFood.Enabled = true;

                return;
            }
            if (CheckId != null)
            {
                MessageBox.Show("Duplicate Id, Please try another Id", "Error", MessageBoxButtons.OK);
                btnAddFood.Enabled = true;
                return;
            }
            var _food = new Product();
            _food.ProductId = ProductId;
            _food.NameProduct = FoodName;
            _food.CateId = CateId;
            _food.Price = Price;

            FoodRepo.Create(_food);

            var listFood = FoodRepo.GetAll().ToList();
            dtgvFood.DataSource = listFood;
            btnAddFood.Enabled = true;
            ResetFormFood();
        }
        private void ResetFormFood()
        {
            txtFoodID.Text = "";
            txtFoodName.Text = "";
            mnFoodPrice.Value = 0;
            txtFoodID.Enabled = true;
            cbFoodCategory.SelectedIndex = 0;
            btnAddFood.Enabled = true;
            btnEditFood.Enabled = false;
            btnDeleteFood.Enabled = false;
        }
        private bool CheckFoodNull()
        {
            if (txtFoodID.Text == "" || txtFoodName.Text == "")
            {
                MessageBox.Show("All Input is not Null, please try again", "Notification", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            var FoodRepo = new RepositoryBase<Product>();
            var list = FoodRepo.GetAll().Where(p => p.NameProduct.Contains(txtSearchFoodName.Text)).ToList();
            if (list != null)
            {
                dtgvFood.DataSource = list;
            }
            else
            {
                MessageBox.Show("Cannot Find Any Food. Please Try again !", "Notification", MessageBoxButtons.OK);
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            if (!CheckFoodNull())
            {
                return;
            }
            btnEditFood.Enabled = false;
            var ProductId = int.Parse(txtFoodID.Text);
            var FoodName = txtFoodName.Text;
            var Price = double.Parse(mnFoodPrice.Value.ToString());
            var CateId = int.Parse(cbFoodCategory.SelectedValue.ToString());

            var FoodRepo = new RepositoryBase<Product>();
            var _food = FoodRepo.GetAll().Where(p => p.ProductId == ProductId).FirstOrDefault();

            if (_food != null)
            {
                _food.ProductId = ProductId;
                _food.NameProduct = FoodName;
                _food.CateId = CateId;
                _food.Price = Price;
                FoodRepo.Update(_food);
            }

            var listFood = FoodRepo.GetAll().ToList();
            dtgvFood.DataSource = listFood;
            txtFoodID.Enabled = true;
            btnEditFood.Enabled = true;
            btnShowFood_Click(sender, e);
            ResetFormFood();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            btnDeleteFood.Enabled = false;
            var ProductId = int.Parse(txtFoodID.Text);
            var FoodRepo = new RepositoryBase<Product>();
            var obj = FoodRepo.GetAll().Where(p => p.ProductId == ProductId).FirstOrDefault();
            if (obj != null)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to Delete ", "Delete Item", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    FoodRepo.Delete(obj);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do nothing
                }
                ResetFormFood();
            }

            var listFood = FoodRepo.GetAll().ToList();
            dtgvFood.DataSource = listFood;

            btnDeleteFood.Enabled = true;
            txtFoodID.Enabled = true;
        }

        private void dtgvFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //get list accountType to combobox
            var CateRepo = new RepositoryBase<Category>();
            

            //e.RowIndex; // dong duoc select
            if (e.RowIndex >= 0)
            {
                txtFoodID.Enabled = false;
                var rowSelected = this.dtgvFood.Rows[e.RowIndex];
                txtFoodID.Text = rowSelected.Cells["ProductID"].Value.ToString();
                txtFoodName.Text = rowSelected.Cells["NameProduct"].Value.ToString();
                cbFoodCategory.SelectedValue = rowSelected.Cells["CateName"].Value.ToString();
                mnFoodPrice.Value = Convert.ToDecimal(rowSelected.Cells["Price"].Value.ToString());
                
            }
            btnAddFood.Enabled = false;
            btnDeleteFood.Enabled = true;
            btnEditFood.Enabled = true;
        }

        private void tcAdmin_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void tcAdmin_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnEditFood.Enabled = false;
            btnDeleteFood.Enabled = false;
        }
    }
    }
    
