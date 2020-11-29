using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleConnectionToDatabase.StoreDb
{
    public class StoreDbContext
    {
        private const string ConnectionString = @"Data Source=DESKTOP-RT3NQPU\SQLEXPRESS; Initial Catalog=MyOrdersDB; Integrated Security=True";
        private SqlConnection _sqlConnection;

        public void TestConnection()
        {
            _sqlConnection = new SqlConnection(ConnectionString);
            _sqlConnection.Open();
            MessageBox.Show("Opened!");
            _sqlConnection.Close();
        }
    }
}