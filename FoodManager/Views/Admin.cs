using Microsoft.VisualBasic.ApplicationServices;
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
using User = Repository.Models.User;

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
            }

        }

        #region methods Category
        void loadDataCate()
        {
            var CategoryRepo = new RepositoryBase<Category>();
            var listCategory = CategoryRepo.GetAll().ToList();
            dtgvCategory.DataSource = listCategory;
        }

        void ResetFormCate()
        {
            txtCategoryID.Text = "";
            txtNameCategory.Text = "";

            btnDeleteFood.Enabled = true;
            btnEditCategory.Enabled = false;
            btnAddCategory.Enabled = true;
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

        bool CheckNull1Cate()
        {
            if (txtNameCategory.Text == "")
            {
                MessageBox.Show("Tất cả đầu vào không phải là Null, vui lòng thử lại", "Thông báo", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        void searchCate()
        {
            var CategoryRepo = new RepositoryBase<Category>();
            var listCategory = CategoryRepo.GetAll().Where(e => e.CateName.Contains(txtSearchName.Text)).ToList();
            if (listCategory != null)
            {
                dtgvCategory.DataSource = listCategory;
            }
            else
            {
                MessageBox.Show("Không thể tìm thấy bất kỳ danh mục nào. Vui lòng thử lại !", "Thông báo", MessageBoxButtons.OK);
            }
            ResetFormCate();
        }

        void addCate()
        {
            if (!CheckNull1Cate())
            {
                return;
            }
            var CateName = txtNameCategory.Text.ToString();
            var CategoryRepo = new RepositoryBase<Category>();
            var Category = new Category();
            Category.CateName = CateName;
            CategoryRepo.Create(Category);
            var ListCategory = CategoryRepo.GetAll();
            dtgvCategory.DataSource = ListCategory;
            ResetFormCate();
        }

        void deleteCate()
        {
            btnDeleteCategory.Enabled = false;
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

            var listCategory = CategoryRepo.GetAll().ToList();
            dtgvCategory.DataSource = listCategory;
            ResetFormCate();
            btnDeleteCategory.Enabled = true;
            txtCategoryID.Enabled = true;
        }

        void editCate()
        {
            if (!CheckNullCate())
            {
                return;
            }
            btnEditCategory.Enabled = false;
            var CateID = txtCategoryID.Text;
            var CateName = txtNameCategory.Text;
            var CategoryRepo = new RepositoryBase<Category>();
            var cate = CategoryRepo.GetAll().Where(e => e.CateId.ToString().Equals(CateID)).FirstOrDefault();

            if (cate != null)
            {
                cate.CateName = CateName;
                CategoryRepo.Update(cate);
            }

            var listCate = CategoryRepo.GetAll().ToList();
            dtgvCategory.DataSource = listCate;

            txtCategoryID.Enabled = true;
            btnEditCategory.Enabled = true;
            ResetFormCate();
        }

        #endregion

        #region events Category
        private void dtgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtgvCategory_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var CategoryRepo = new RepositoryBase<Category>();
            var listCategory = CategoryRepo.GetAll().ToList().Select(e => new { Text = e.CateId, Value = e.CateName }).ToList();
            cbFoodCategory.ValueMember = "Text";
            cbFoodCategory.DisplayMember = "Value";
            cbFoodCategory.DataSource = (listCategory.ToArray());

            if (e.RowIndex >= 0)
            {
                txtCategoryID.Enabled = false;
                var rowSelected = this.dtgvCategory.Rows[e.RowIndex];
                txtCategoryID.Text = rowSelected.Cells["CateId"].Value.ToString();
                txtNameCategory.Text = rowSelected.Cells["CateName"].Value.ToString();
                cbFoodCategory.SelectedValue = rowSelected.Cells["CateId"].Value.ToString();
            }
            btnAddCategory.Enabled = false;
            btnDeleteCategory.Enabled = true;
            btnEditCategory.Enabled = true;
        }




        private void btnSearchCate_Click(object sender, EventArgs e)
        {
            searchCate();
        }

        private void btnResetForm_Click(object sender, EventArgs e)
        {
            ResetFormCate();
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
    }
}
