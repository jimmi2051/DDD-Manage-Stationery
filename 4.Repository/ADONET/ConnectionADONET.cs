using System.Data.SqlClient;
using MyProject.Infrastructure;
using System.Data;

namespace MyProject.Repository.ADONET
{
    public class ConnectionADONET
    {
        
        private static SqlConnection connection;
        public static SqlConnection Connection
        {
            get {
                if (connection == null)
                    connection = new SqlConnection(Information.ConnectionString);
                if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
                {
                    connection.Open();
                }

                return connection;
            }
        }
    }
}
