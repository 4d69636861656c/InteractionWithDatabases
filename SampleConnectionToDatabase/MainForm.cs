using System;
using System.Windows.Forms;
using SampleConnectionToDatabase.Models;
using SampleConnectionToDatabase.StoreDb;

namespace SampleConnectionToDatabase
{
    using System.Collections;
    using System.Data;
    using System.Linq;

    public partial class MainForm : Form
    {
        private readonly StoreDbContext _storeDbContext;

        private User _user;

        private Product _product;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.usersDataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.OnUsersRowHeaderMouseClick);
            this.productsDataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.OnProductsRowHeaderMouseClick);

            this._storeDbContext = new StoreDbContext();
            this._user = new User();
            this._product = new Product();

            this.PopulateUsersDataGrid();
            this.PopulateProductsDataGrid();
        }
        
        /// <summary>
        /// Populates users data grid view with a table from the database.
        /// </summary>
        private void PopulateUsersDataGrid()
        {
            BindingSource bindingSource = new BindingSource
            {
                DataSource = _storeDbContext.ReadTable(TableNameEnum.Users)
            };

            usersDataGridView.DataSource = bindingSource;
        }

        /// <summary>
        /// Populates users data grid view with a table from the database.
        /// </summary>
        private void PopulateProductsDataGrid()
        {
            BindingSource bindingSource = new BindingSource
            {
                DataSource = _storeDbContext.ReadTable(TableNameEnum.Products)
            };

            productsDataGridView.DataSource = bindingSource;

            // Display the category names in the dropdown menu.
            this.productCategoryComboBox.DataSource = this._storeDbContext.ReadTable(TableNameEnum.Categories).DefaultView;
            this.productCategoryComboBox.DisplayMember = "Name";
        }

        #region Users
        private void CreateUserClick(object sender, EventArgs e)
        {
            User user = new User()
            {
                UserName = userNameTextBox.Text,
                UserPassword = userPasswordTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text,
                CreatedOn = DateTime.Now,
                Active = 0
            };

            userNameTextBox.Text = string.Empty;
            userPasswordTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            phoneTextBox.Text = string.Empty;

            if (_storeDbContext.InsertUser(user))
            {
                PopulateUsersDataGrid();
                MessageBox.Show("A new user was created!", "User Added!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        private void OnUsersRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (usersDataGridView.Rows[e.RowIndex].Cells[0].Value is null || string.IsNullOrWhiteSpace(usersDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                return;
            }

            _user.UserId = Convert.ToInt32(usersDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            _user.UserName = userNameTextBox.Text = usersDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            _user.UserPassword = userPasswordTextBox.Text = usersDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            _user.Email = emailTextBox.Text = usersDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            _user.Phone = phoneTextBox.Text = usersDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            _user.CreatedOn = Convert.ToDateTime(usersDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
            _user.Active = Convert.ToInt32(usersDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());

            //MessageBox.Show("Clicked on a row header!");
        }

        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                UserId = _user.UserId,
                UserName = userNameTextBox.Text,
                UserPassword = userPasswordTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text,
                CreatedOn = _user.CreatedOn,
                Active = _user.Active
            };

            userNameTextBox.Text = string.Empty;
            userPasswordTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            phoneTextBox.Text = string.Empty;

            if (_storeDbContext.UpdateUser(user))
            {
                PopulateUsersDataGrid();
                MessageBox.Show($"The user \"{user.UserName}\" was updated!", "User Updated!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            userNameTextBox.Text = string.Empty;
            userPasswordTextBox.Text = string.Empty;
            emailTextBox.Text = string.Empty;
            phoneTextBox.Text = string.Empty;

            if (_storeDbContext.DeleteRow(TableNameEnum.Users, _user.UserId))
            {
                PopulateUsersDataGrid();
                MessageBox.Show($"The user \"{_user.UserName}\" was deleted from the database!", "User deleted!");
                _user = new User();
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }
        #endregion

        #region Products
        private void CreateProductButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = this._storeDbContext.ReadTable(TableNameEnum.Categories);
            int categoryId = dataTable.Rows[this.productCategoryComboBox.SelectedIndex].Field<int>("CategoryID");

            Product product = new Product()
                            {
                                CategoryId = categoryId,
                                Name = this.productNameTextBox.Text,
                                Price = this.productPriceTextBox.Text,
                                Discount = this.productDiscountTextBox.Text,
                                Description = this.productDescriptionTextBox.Text,
                                CreatedOn = DateTime.Now,
                                EditedOn = DateTime.Now
                            };

            this.productPriceTextBox.Text = string.Empty;
            this.productPriceTextBox.Text = string.Empty;
            this.productDiscountTextBox.Text = string.Empty;
            this.productDescriptionTextBox.Text = string.Empty;

            if (_storeDbContext.InsertProduct(product))
            {
                this.PopulateProductsDataGrid();
                MessageBox.Show("A new product was added!", "Product Added!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        private void OnProductsRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.productsDataGridView.Rows[e.RowIndex].Cells[0].Value is null || string.IsNullOrWhiteSpace(this.productsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                return;
            }

            DataTable dataTable = this._storeDbContext.ReadTable(TableNameEnum.Categories);

            // Forgot what it was supposed to store.
            //EnumerableRowCollection<DataRow> filtered = dataTable.AsEnumerable().Where(r => r.Field<string>("CategoryName").Contains(this.productsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()));
            
            // Select the appropriate category for the selected product.
            this.productCategoryComboBox.SelectedIndex = this.productCategoryComboBox.FindStringExact(this.productsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());

            // Fill in the product properties and UI text boxes.
            this._product.ProductId = Convert.ToInt32(this.productsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            this._product.Name = this.productNameTextBox.Text = this.productsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            this._product.CategoryId = dataTable.Rows[this.productCategoryComboBox.SelectedIndex].Field<int>("CategoryID");
            this._product.Price = this.productPriceTextBox.Text = this.productsDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            this._product.Discount = this.productDiscountTextBox.Text = this.productsDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            this._product.Description = this.productDescriptionTextBox.Text = this.productsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
            this._product.CreatedOn = Convert.ToDateTime(this.productsDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());
            this._product.EditedOn = Convert.ToDateTime(this.productsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());

            //MessageBox.Show("Clicked on a row header!");
        }

        private void UpdateProductButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = this._storeDbContext.ReadTable(TableNameEnum.Categories);
            this._product.CategoryId = dataTable.Rows[this.productCategoryComboBox.SelectedIndex].Field<int>("CategoryID");

            Product product = new Product()
                            {
                                ProductId = this._product.ProductId,
                                CategoryId = this._product.CategoryId,
                                Name = this.productNameTextBox.Text,
                                Price = this.productPriceTextBox.Text,
                                Discount = this.productDiscountTextBox.Text,
                                Description = this.productDescriptionTextBox.Text,
                                EditedOn = DateTime.Now
                            };

            this.productNameTextBox.Text = string.Empty;
            this.productPriceTextBox.Text = string.Empty;
            this.productDiscountTextBox.Text = string.Empty;
            this.productDescriptionTextBox.Text = string.Empty;

            if (_storeDbContext.UpdateProduct(product))
            {
                this.PopulateProductsDataGrid();
                MessageBox.Show($"Product \"{product.Name}\" was updated successfully!", "Product Updated!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }
        #endregion
    }
}
