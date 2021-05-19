namespace SampleConnectionToDatabase
{
    using Models;
    using StoreDb;
    using System;
    using System.Data;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        /// <summary>
        /// The store DB context.
        /// </summary>
        private readonly StoreDbContext _storeDbContext;

        /// <summary>
        /// The user.
        /// </summary>
        private User _user;

        /// <summary>
        /// The product.
        /// </summary>
        private Product _product;

        /// <summary>
        /// The category.
        /// </summary>
        private Category _category;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.usersDataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.OnUsersRowHeaderMouseClick);
            this.productsDataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.OnProductsRowHeaderMouseClick);
            this.categoriesDataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.OnCategoriesRowHeaderMouseClick);

            this._storeDbContext = new StoreDbContext();
            this._user = new User();
            this._product = new Product();
            this._category = new Category();

            this.PopulateUsersDataGrid();
            this.PopulateProductsDataGrid();
            this.PopulateCategoriesDataGrid();
        }

        #region Fill Tables

        /// <summary>
        /// Populates users data grid view with a table from the database.
        /// </summary>
        private void PopulateUsersDataGrid()
        {
            BindingSource bindingSource = new BindingSource
            {
                DataSource = _storeDbContext.ReadTable(TableNameEnum.Users)
            };

            this.usersDataGridView.DataSource = bindingSource;
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

            this.productsDataGridView.DataSource = bindingSource;

            // Display the category names in the dropdown menu.
            this.productCategoryComboBox.DataSource = this._storeDbContext.ReadTable(TableNameEnum.Categories).DefaultView;
            this.productCategoryComboBox.DisplayMember = "Name";
        }

        /// <summary>
        /// Populates categories data grid view with a table from the database.
        /// </summary>
        private void PopulateCategoriesDataGrid()
        {
            BindingSource bindingSource = new BindingSource
            {
                DataSource = this._storeDbContext.ReadTable(TableNameEnum.Categories)
            };

            this.categoriesDataGridView.DataSource = bindingSource;
        }

        #endregion Fill Tables

        #region Users
        /// <summary>
        /// Event handler for the Create User button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CreateUserClick(object sender, EventArgs e)
        {
            User user = new User()
            {
                UserName = this.userNameTextBox.Text,
                UserPassword = this.userPasswordTextBox.Text,
                Email = this.emailTextBox.Text,
                Phone = this.phoneTextBox.Text,
                CreatedOn = DateTime.Now,
                Active = 0
            };

            this.userNameTextBox.Text = string.Empty;
            this.userPasswordTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.phoneTextBox.Text = string.Empty;

            if (this._storeDbContext.InsertUser(user))
            {
                this.PopulateUsersDataGrid();
                MessageBox.Show("A new user was created!", "User Added!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        /// <summary>
        /// Event handler for the Users DataGrid row header.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void OnUsersRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.usersDataGridView.Rows[e.RowIndex].Cells[0].Value is null || string.IsNullOrWhiteSpace(this.usersDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                return;
            }

            this._user.UserId = Convert.ToInt32(this.usersDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            this._user.UserName = this.userNameTextBox.Text = this.usersDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            this._user.UserPassword = this.userPasswordTextBox.Text = this.usersDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            this._user.Email = this.emailTextBox.Text = this.usersDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            this._user.Phone = this.phoneTextBox.Text = this.usersDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            this._user.CreatedOn = Convert.ToDateTime(this.usersDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
            this._user.Active = Convert.ToInt32(this.usersDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());

            //MessageBox.Show("Clicked on a row header!");
        }

        /// <summary>
        /// Event handler for the Update User button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void UpdateUserButton_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                UserId = this._user.UserId,
                UserName = this.userNameTextBox.Text,
                UserPassword = this.userPasswordTextBox.Text,
                Email = this.emailTextBox.Text,
                Phone = this.phoneTextBox.Text,
                CreatedOn = this._user.CreatedOn,
                Active = this._user.Active
            };

            this.userNameTextBox.Text = string.Empty;
            this.userPasswordTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.phoneTextBox.Text = string.Empty;

            if (this._storeDbContext.UpdateUser(user))
            {
                this.PopulateUsersDataGrid();
                MessageBox.Show($"The user \"{user.UserName}\" was updated!", "User Updated!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        /// <summary>
        /// Event handler for the Delete User button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DeleteUserButton_Click(object sender, EventArgs e)
        {
            this.userNameTextBox.Text = string.Empty;
            this.userPasswordTextBox.Text = string.Empty;
            this.emailTextBox.Text = string.Empty;
            this.phoneTextBox.Text = string.Empty;

            if (this._storeDbContext.DeleteRow(TableNameEnum.Users, this._user.UserId))
            {
                this.PopulateUsersDataGrid();
                MessageBox.Show($"The user \"{_user.UserName}\" was deleted from the database!", "User deleted!");
                this._user = new User();
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        #endregion Users

        #region Products
        /// <summary>
        /// Event handler for the Create Product button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
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

            if (this._storeDbContext.InsertProduct(product))
            {
                this.PopulateProductsDataGrid();
                MessageBox.Show("A new product was added!", "Product Added!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        /// <summary>
        /// Event handler for the Products DataGrid row header.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
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

        /// <summary>
        /// Event handler for the Update Product button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
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

            if (this._storeDbContext.UpdateProduct(product))
            {
                this.PopulateProductsDataGrid();
                MessageBox.Show($"Product \"{product.Name}\" was updated successfully!", "Product Updated!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        /// <summary>
        /// Event handler for the Delete Product button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DeleteProductButton_Click(object sender, EventArgs e)
        {
            this.productNameTextBox.Text = string.Empty;
            this.productPriceTextBox.Text = string.Empty;
            this.productDiscountTextBox.Text = string.Empty;
            this.productDescriptionTextBox.Text = string.Empty;

            if (this._storeDbContext.DeleteRow(TableNameEnum.Products, this._product.ProductId))
            {
                this.PopulateProductsDataGrid();
                MessageBox.Show($"The product \"{_user.UserName}\" was deleted from the database!", "Product deleted!");
                this._product = new Product();
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        #endregion Products

        #region Categories
        /// <summary>
        /// Event handler for the Create Category button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void CreateCategoryButton_Click(object sender, EventArgs e)
        {
            Category category = new Category()
            {
                Name = this.categoryNameTextBox.Text,
                Description = this.categoryDescriptionTextBox.Text,
                CreatedOn = DateTime.Now
            };

            this.categoryNameTextBox.Text = string.Empty;
            this.categoryDescriptionTextBox.Text = string.Empty;

            if (this._storeDbContext.InsertCategory(category))
            {
                this.PopulateCategoriesDataGrid();
                MessageBox.Show("A new category was created!", "Category Added!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        /// <summary>
        /// Event handler for the Categories DataGrid row header.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void OnCategoriesRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.categoriesDataGridView.Rows[e.RowIndex].Cells[0].Value is null || string.IsNullOrWhiteSpace(this.categoriesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                return;
            }

            this._category.CategoryId = Convert.ToInt32(this.categoriesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            this._category.Name = this.categoryNameTextBox.Text = this.categoriesDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            this._category.Description = this.categoryDescriptionTextBox.Text = this.categoriesDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            this._category.CreatedOn = Convert.ToDateTime(this.categoriesDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());

            //MessageBox.Show("Clicked on a row header!");
        }

        /// <summary>
        /// Event handler for the Update Category button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void UpdateCategoryButton_Click(object sender, EventArgs e)
        {
            Category category = new Category()
            {
                CategoryId = this._category.CategoryId,
                Name = this.categoryNameTextBox.Text,
                Description = this.categoryDescriptionTextBox.Text,
                CreatedOn = this._category.CreatedOn
            };

            this.categoryNameTextBox.Text = string.Empty;
            this.categoryDescriptionTextBox.Text = string.Empty;

            if (this._storeDbContext.UpdateCategory(category))
            {
                this.PopulateCategoriesDataGrid();
                MessageBox.Show($"The category \"{category.Name}\" was updated!", "Category Updated!");
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        /// <summary>
        /// Event handler for the Delete Category button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event.</param>
        private void DeleteCategoryButton_Click(object sender, EventArgs e)
        {
            this.categoryNameTextBox.Text = string.Empty;
            this.categoryDescriptionTextBox.Text = string.Empty;

            if (this._storeDbContext.DeleteRow(TableNameEnum.Categories, _category.CategoryId))
            {
                this.PopulateCategoriesDataGrid();
                MessageBox.Show($"The category \"{_category.Name}\" was deleted from the database!", "Category deleted!");
                this._category = new Category();
            }
            else
            {
                MessageBox.Show("Input Error!\n Please try again!", "Input Error!");
            }
        }

        #endregion Categories
    }
}