using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSQLConnection.Connections
{
    public class DbConnection
    {

        #region Private Properties

        private SqlConnection _dbConn;

        #endregion Private Properties


        #region Constructor

        public DbConnection(string connectionString)
        {
            this._dbConn = new SqlConnection(connectionString);
        }

        #endregion Constructor


        #region Public Methods

        public void ExecuteCommand(string queryString)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand(queryString, _dbConn);
                sqlCommand.Connection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch(SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods
    }
}
