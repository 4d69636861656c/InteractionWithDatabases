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
        private const string ConnectionString = @"Data Source=DESKTOP-RT3NQPU\SQLEXPRESS; Initial Catalog=MyOrdersDB; Integrated Security=True";
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

        #region Read From Table 
        /// <summary>
        /// Reads all the rows in a table.
        /// </summary>
        /// <param name="table">Table name.</param>
        /// <returns>A <see cref="DataTable"/> object.</returns>
        public DataTable ReadTable(TableNameEnum table)
        {
            string tableName = GetTableName(table);

            _sqlConnection.Open();
            using (SqlCommand selectCommand = new SqlCommand($"SELECT * FROM {tableName}", _sqlConnection))
            {
                selectCommand.CommandType = CommandType.Text;
                using (SqlDataAdapter sda = new SqlDataAdapter(selectCommand))
                {
                    using (DataTable dataTable = new DataTable())
                    {
                        sda.Fill(dataTable);
                        _sqlConnection.Close();

                        return dataTable;
                    }
                }
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

            _sqlConnection.Open();
            using (SqlCommand insertCommand = new SqlCommand($"IF OBJECTPROPERTY(OBJECT_ID('dbo.OrderData'), 'TableHasIdentity') = 1\r\n" +
                                                             $"SET IDENTITY_INSERT [dbo].[OrderData] ON " +
                                                             $"INSERT INTO Users (UserName, UserPassword, Email, Phone, CreatedOn, Active) " +
                                                             $"VALUES (@UserName, @UserPassword, @Email, @Phone, @CreatedOn, @Active) " +
                                                             $"SET IDENTITY_INSERT [dbo].[OrderData] OFF", _sqlConnection))
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
                    _sqlConnection.Close();

                    return rowsAdded > 0;
                }
            }
        }
        #endregion

        #region Update Row
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

            _sqlConnection.Open();
            using (SqlCommand updateCommand = new SqlCommand($"UPDATE Users\r\nSET Users.UserName = @UserName, Users.UserPassword = @UserPassword, Users.Email = @Email, Users.Phone = @Phone, Users.CreatedOn = @CreatedOn, Users.Active = @Active\r\nWHERE Users.UserID = @UserId;", _sqlConnection))
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
                    _sqlConnection.Close();

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
        #endregion
    }
}