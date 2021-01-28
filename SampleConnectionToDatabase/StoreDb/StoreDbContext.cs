using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using SampleConnectionToDatabase.Models;

namespace SampleConnectionToDatabase.StoreDb
{
    public class StoreDbContext
    {
        #region Private Members 
        private const string ConnectionString = "Data Source=DESKTOP-LOFN77V; Initial Catalog=MyOrdersDB; Integrated Security=True";
        private readonly SqlConnection _sqlConnection;
        #endregion

        #region Constructors and Finalizer 
        private StoreDbContext(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            connectionString = ConnectionString;
            _sqlConnection = new SqlConnection(connectionString);
        }

        public StoreDbContext() : this(ConnectionString) { }

        ~StoreDbContext()
        {
            _sqlConnection.Dispose();
        }
        #endregion

        #region Public Methods

        #region Read From Tables 
        /// <summary>
        /// Reads all the rows in a table.
        /// </summary>
        /// <param name="table">Table name.</param>
        /// <returns>A <see cref="DataTable"/> object.</returns>
        public DataTable ReadTable(TableNameEnum table)
        {
            string tableName = GetTableName(table);

            if (table == TableNameEnum.Users || table == TableNameEnum.Categories)
            {
                this._sqlConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand($"SELECT * FROM {tableName}", this._sqlConnection))
                {
                    selectCommand.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(selectCommand))
                    {
                        using (DataTable dataTable = new DataTable())
                        {
                            sda.Fill(dataTable);
                            this._sqlConnection.Close();

                            return dataTable;
                        }
                    }
                }
            }
            else
            {
                return this.ReadProductsTable();
            }
        }
        #endregion

        #region Insert Into Tables
        /// <summary>
        /// Adds a new <see cref="User"/> to the table.
        /// </summary>
        /// <param name="user">The <see cref="User"/> instance to be added to the table.</param>
        /// <returns>True if the method succeeded, false otherwise.</returns>
        public bool InsertUser(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.UserPassword) || string.IsNullOrEmpty(user.Email) || !(Regex.IsMatch(user.Phone, @"^\d+$")))
            {
                return false;
            }

            this._sqlConnection.Open();
            using (SqlCommand insertCommand = new SqlCommand($"IF OBJECTPROPERTY(OBJECT_ID('dbo.Users'), 'TableHasIdentity') = 1\r\n"
                                                             /*+$"SET IDENTITY_INSERT [dbo].[Users] ON " +*/
                                                             +$"INSERT INTO Users (UserName, UserPassword, Email, Phone, CreatedOn, Active) "
                                                             + $"VALUES (@UserName, @UserPassword, @Email, @Phone, @CreatedOn, @Active) "
                                                             /*$"SET IDENTITY_INSERT [dbo].[Users] OFF"*/, _sqlConnection))
            {
                insertCommand.CommandType = CommandType.Text;
                using (new SqlDataAdapter(insertCommand))
                {
                    insertCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    insertCommand.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                    insertCommand.Parameters.AddWithValue("@Email", user.Email);
                    insertCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    insertCommand.Parameters.AddWithValue("@CreatedOn", user.CreatedOn);
                    insertCommand.Parameters.AddWithValue("@Active", user.Active);

                    int rowsAdded = insertCommand.ExecuteNonQuery();
                    this._sqlConnection.Close();

                    return rowsAdded > 0;
                }
            }
        }

        public bool InsertProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Price) || !double.TryParse(product.Price, out _) || !double.TryParse(product.Discount, out _))
            {
                return false;
            }

            this._sqlConnection.Open();
            using (SqlCommand insertCommand = new SqlCommand($"IF OBJECTPROPERTY(OBJECT_ID('dbo.Products'), 'TableHasIdentity') = 1\r\n"
                                                             /*+ $"SET IDENTITY_INSERT [dbo].[Products] ON "*/
                                                             + $"INSERT INTO Products (CategoryID, Name, Price, Discount, Description, CreatedOn, EditedOn)\r\n"
                                                             + $"VALUES (@CategoryId, @Name, @Price, @Discount, @Description, @CreatedOn, @EditedOn)\r\n"
                                                             /*+ $"SET IDENTITY_INSERT [dbo].[Products] OFF"*/, _sqlConnection))
            {
                insertCommand.CommandType = CommandType.Text;
                using (new SqlDataAdapter(insertCommand))
                {
                    insertCommand.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    insertCommand.Parameters.AddWithValue("@Name", product.Name);
                    insertCommand.Parameters.AddWithValue("@Price", product.Price);
                    insertCommand.Parameters.AddWithValue("@Discount", product.Discount);
                    insertCommand.Parameters.AddWithValue("@Description", product.Description);
                    insertCommand.Parameters.AddWithValue("@CreatedOn", product.CreatedOn);
                    insertCommand.Parameters.AddWithValue("@EditedOn", product.EditedOn);

                    int rowsAdded = insertCommand.ExecuteNonQuery();
                    this._sqlConnection.Close();

                    return rowsAdded > 0;
                }
            }
        }
        #endregion

        #region Update Rows
        /// <summary>
        /// Updates a row from the users table.
        /// </summary>
        /// <param name="user">The <see cref="User"/> instance to be updated.</param>
        /// <returns>True if the method succeeded, false otherwise.</returns>
        public bool UpdateUser(User user)
        {
            if (user.UserId == 0 || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.UserPassword) || string.IsNullOrEmpty(user.Email) || !(Regex.IsMatch(user.Phone, @"^\d+$")))
            {
                return false;
            }

            this._sqlConnection.Open();
            using (SqlCommand updateCommand = new SqlCommand($"UPDATE Users\r\n"
                                                             + $"SET Users.UserName = @UserName, Users.UserPassword = @UserPassword, "
                                                             + $"Users.Email = @Email, Users.Phone = @Phone, Users.CreatedOn = @CreatedOn, Users.Active = @Active\r\n"
                                                             + $"WHERE Users.UserID = @UserId;", _sqlConnection))
            {
                updateCommand.CommandType = CommandType.Text;
                using (new SqlDataAdapter(updateCommand))
                {
                    updateCommand.Parameters.AddWithValue("@UserId", user.UserId);
                    updateCommand.Parameters.AddWithValue("@UserName", user.UserName);
                    updateCommand.Parameters.AddWithValue("@UserPassword", user.UserPassword);
                    updateCommand.Parameters.AddWithValue("@Email", user.Email);
                    updateCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    updateCommand.Parameters.AddWithValue("@CreatedOn", user.CreatedOn);
                    updateCommand.Parameters.AddWithValue("@Active", user.Active);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    this._sqlConnection.Close();

                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Price) || !double.TryParse(product.Price, out _) || !double.TryParse(product.Discount, out _))
            {
                return false;
            }

            this._sqlConnection.Open();
            using (SqlCommand updateCommand = new SqlCommand($"UPDATE Products\r\n"
                                                             + $"SET Products.CategoryID = @CategoryId, Products.Name = @Name, Products.Price = @Price, "
                                                             + $"Products.Discount = @Discount, Products.Description = @Description, "
                                                             + $"Products.EditedOn = @EditedOn\r\n"
                                                             + $"WHERE Products.ProductID = @ProductId;", _sqlConnection))
            {
                updateCommand.CommandType = CommandType.Text;
                using (new SqlDataAdapter(updateCommand))
                {
                    updateCommand.Parameters.AddWithValue("@ProductId", product.ProductId);
                    updateCommand.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    updateCommand.Parameters.AddWithValue("@Name", product.Name);
                    updateCommand.Parameters.AddWithValue("@Price", product.Price);
                    updateCommand.Parameters.AddWithValue("@Discount", product.Discount);
                    updateCommand.Parameters.AddWithValue("@Description", product.Description);
                    updateCommand.Parameters.AddWithValue("@EditedOn", product.EditedOn);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    this._sqlConnection.Close();

                    return rowsAffected > 0;
                }
            }
        }
        #endregion

        #region Delete Row
        /// <summary>
        /// Deletes a row from the table.
        /// </summary>
        /// <param name="table">Table name.</param>
        /// <param name="id">Row ID.</param>
        /// <returns>True if the method succeeded, false otherwise.</returns>
        public bool DeleteRow(TableNameEnum table, int id)
        {
            string tableName = GetTableName(table);

            string tablePrimaryKey;

            switch (table)
            {
                case TableNameEnum.Categories:
                    tablePrimaryKey = "CategoryID";
                    break;
                case TableNameEnum.CustomerAddresses:
                    tablePrimaryKey = "CustomerAddressID";
                    break;
                case TableNameEnum.OrderData:
                    tablePrimaryKey = "OrderID";
                    break;
                case TableNameEnum.Products:
                    tablePrimaryKey = "ProductID";
                    break;
                case TableNameEnum.ProductVariants:
                    tablePrimaryKey = "ProductVariantID";
                    break;
                case TableNameEnum.Users:
                default:
                    tablePrimaryKey = "UserID";
                    break;
            }

            _sqlConnection.Open();

            try
            {
                using (SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {tableName} WHERE {tableName}.{tablePrimaryKey} = {id};", _sqlConnection))
                {
                    deleteCommand.CommandType = CommandType.Text;
                    using (new SqlDataAdapter(deleteCommand))
                    {
                        // Doesn't work. 
                        //deleteCommand.Parameters.AddWithValue("@Id", id);
                        //deleteCommand.Parameters.AddWithValue("@TableName", tableName);
                        //deleteCommand.Parameters.AddWithValue("@TablePrimaryKey", tablePrimaryKey);

                        int rowsDeleted = deleteCommand.ExecuteNonQuery();
                        _sqlConnection.Close();

                        return rowsDeleted > 0;
                    }
                }
            }
            catch
            {
                _sqlConnection.Close();
                return false;
            }
        }
        #endregion

        #endregion

        #region Private Methods
        private static string GetTableName(TableNameEnum table)
        {
            string tableName;

            switch (table)
            {
                case TableNameEnum.Categories:
                    tableName = "Category";
                    break;
                case TableNameEnum.CustomerAddresses:
                    tableName = "CustomerAddress";
                    break;
                case TableNameEnum.OrderData:
                    tableName = "OrderData";
                    break;
                case TableNameEnum.OrderDetails:
                    tableName = "OrderDetails";
                    break;
                case TableNameEnum.Products:
                    tableName = "Products";
                    break;
                case TableNameEnum.ProductVariants:
                    tableName = "ProductVariant";
                    break;
                case TableNameEnum.Users:
                    tableName = "Users";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(table), table, null);
            }

            return tableName;
        }

        private DataTable ReadProductsTable()
        {
            this._sqlConnection.Open();
            using (SqlCommand selectCommand = new SqlCommand($"SELECT \r\n\tp.ProductID,\r\n\tp.Name,\r\n\tc.Name,\r\n\tp.Price,\r\n\tp.Discount,\r\n\tp.Description,\r\n\tp.CreatedOn,\r\n\tp.EditedOn\r\nFROM\r\ndbo.Products p\r\nINNER JOIN Category c \r\nON c.CategoryID = p.CategoryID\r\nORDER BY\r\np.Name DESC;", this._sqlConnection))
            {
                selectCommand.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(selectCommand))
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        sda.Fill(dataTable);
                        this._sqlConnection.Close();

                        dataTable.Columns["Name1"].ColumnName = "Category";

                        return dataTable;
                    }
                }
            }
        }
        #endregion
    }
}