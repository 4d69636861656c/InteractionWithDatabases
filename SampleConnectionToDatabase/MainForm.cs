using System;
using System.Windows.Forms;
using SampleConnectionToDatabase.Models;
using SampleConnectionToDatabase.StoreDb;

namespace SampleConnectionToDatabase
{
    public partial class MainForm : Form
    {
        private readonly StoreDbContext _storeDbContext;

        private User _user;

        public MainForm()
        {
            InitializeComponent();
            storeDataGridView.RowHeaderMouseClick += new DataGridViewCellMouseEventHandler(OnRowHeaderMouseClick);

            _storeDbContext = new StoreDbContext();
            _user = new User();

            PopulateUsersDataGrid();
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

            storeDataGridView.DataSource = bindingSource;
        }

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

        private void OnRowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (storeDataGridView.Rows[e.RowIndex].Cells[0].Value is null || string.IsNullOrWhiteSpace(storeDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                return;
            }

            _user.UserId = Convert.ToInt32(storeDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
            _user.UserName = userNameTextBox.Text = storeDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
            _user.UserPassword = userPasswordTextBox.Text = storeDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
            _user.Email = emailTextBox.Text = storeDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
            _user.Phone = phoneTextBox.Text = storeDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
            _user.CreatedOn = Convert.ToDateTime(storeDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString());
            _user.Active = Convert.ToInt32(storeDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString());

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
    }
}
