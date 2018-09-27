using System.Data;
using System.Data.SqlClient;

namespace SocialNetworkConsole.DataAccess
{
    /// <summary>
    /// Contains all database related actions.
    /// </summary>
    public class DbConnection
    {
        private readonly string _dbConnectionString;
        private readonly int _timeoutSeconds;

        /// <summary>
        /// Constructor takes a connection string and timeout.
        /// </summary>
        public DbConnection(string dbConnectionString, int timeoutSeconds)
        {
            _dbConnectionString = dbConnectionString;
            _timeoutSeconds = timeoutSeconds;
        }

        /// <summary>
        /// Tries to establish a connection to the database. Will throw a SqlException if not successful.
        /// </summary>
        public void CheckConnection()
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString)) connection.Open();
        }

        /// <summary>
        /// Executes query and returns DataSet.
        /// </summary>
        /// <param name="query">SQL query.</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteQuery(string query)
        {
            DataSet result = new DataSet();

            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = _timeoutSeconds;
                    command.CommandType = CommandType.Text;

                    // Execute the command and populate data table.
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command)) dataAdapter.Fill(result);
                }
            }

            return result;
        }

        /// <summary>
        /// Executes non query.
        /// </summary>
        /// <param name="query">SQL query.</param>
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(_dbConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = _timeoutSeconds;
                    command.CommandType = CommandType.Text;

                    // Execute the command.
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
